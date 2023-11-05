#define DebagTestFunction

using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace VQTFarm
{
    public partial class MainForm : Form
    {
        #region MainForm Fields
        private FarmSettings fs;    //настройки фермы, заданные в начальном окне (формат флага, время раунда и тп)
        private DataBaseWorkAplication DBWorkForm;  //подключённая бдшка

        #region Threads
        private Thread TeamsUpdateTHR;  //поток обновления данных о командах
        private bool isTeamsUpdateTHRMustRun;   //ОПРОС RoundTime

        private Thread FlagsUpdateTHR;  //поток обновления данных о флагах
        private bool isFlagsUpdateTHRMustRun;   //ОПРОС RoundTime

        private Thread SploitDirectoryCheckTHR; //поток проверки папки Sploits на наличие сплоитов, добавление их в общий процесс
        private bool isSploitDirectoryCheckTHRMustRun;  //ОПРОС GLOBALTimePauseForThreadings

        private Thread AutoSploitRunTHR;    //поток автоматического запуска сплоитов в делай раунд тайма
        private bool isAutoSploitRunTHRMustRun; //ОПРОС RoundTime

        private Thread FailSafeTHR; //поток автозапуска потоков при их непредвиденной остановке
        private bool isFailSafeTHRMustRun;  //ОПРОС GLOBALTimePauseForThreadings

        private Thread FlagsSendTHR;    //поток автоотправки флагов
        private bool isFlagsSendTHRMustRun; //ОПРОС 1 секунда

        private const int FailSafeTHRCoolDown = 20000;
        private const int FlagsSendTHRCoolDown = 200;
        private const int SploitDirectoryCheckTHRCoolDown = 20000;

        private const int SploitTestCoolDown = 10000;
        private const int ManualFlagSendCoolDown = 1000;
        #endregion

        private List<KeyValuePair<string, string>> teamsList;   //Список, содержащий пары IP команды, её название

        private List<string> sploitsList;   //Cписок сплоитов(полный путь)

        private Queue<KeyValuePair<ThreadInfoClass, DateTime>> flagsQueue;  //Очередь на отправку флагов, содержит информацию о полученном флаге и фремя получения этого флага

        private List<string> filtersForFlagShow;

        private string exploitPathForTest;
        private string pathToConnectionCheckScript = "";
        #endregion

        public MainForm(FarmSettings fs, DataBaseWorkAplication DBWorkForm)
        {
            this.fs = fs;
            this.DBWorkForm = DBWorkForm;
            isTeamsUpdateTHRMustRun = true;
            isFlagsUpdateTHRMustRun = true;
            isSploitDirectoryCheckTHRMustRun = true;
            isAutoSploitRunTHRMustRun = true;
            isFailSafeTHRMustRun = true;
            isFlagsSendTHRMustRun = true;

            teamsList = new List<KeyValuePair<string, string>>();
            sploitsList = new List<string>();

            flagsQueue = new Queue<KeyValuePair<ThreadInfoClass, DateTime>>();

            filtersForFlagShow = new List<string>();

            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = "VQT Farm (Starting)";
                teamsToolStripMenuItem.CheckState = CheckState.Checked;
                flagHistoryToolStripMenuItem.CheckState = CheckState.Checked;
                manualSubmitToolStripMenuItem.CheckState = CheckState.Checked;
                exploitTestToolStripMenuItem.CheckState = CheckState.Checked;
                flagShowFilterToolStripMenuItem.CheckState = CheckState.Checked;


                exploitChooseTextBox.Text = "-Choose Exploit for test-";

                teamChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                teamChooseComboBox.Items.Add("-Select Team for test-");
                teamChooseComboBox.SelectedItem = "-Select Team for test-";

                exploitChooseTextBox.Click += new EventHandler(exploitChooseTextBox_Click);
                exploitChooseTextBox.Enter += new EventHandler(exploitChooseTextBox_Enter);
                exploitChooseTextBox.Cursor = Cursors.Arrow;


                exploitFilterTextBox.Text = ">Exploit Name";
                exploitFilterTextBox.ForeColor = Color.Gray;
                exploitFilterTextBox.Enter += new EventHandler(exploitFilterTextBox_Enter);
                exploitFilterTextBox.Leave += new EventHandler(exploitFilterTextBox_Leave);

                teamFilterTextBox.Text = ">Team Name";
                teamFilterTextBox.ForeColor = Color.Gray;
                teamFilterTextBox.Enter += new EventHandler(teamFilterTextBox_Enter);
                teamFilterTextBox.Leave += new EventHandler(teamFilterTextBox_Leave);

                flagFilterTextBox.Text = ">Flag";
                flagFilterTextBox.ForeColor = Color.Gray;
                flagFilterTextBox.Enter += new EventHandler(flagFilterTextBox_Enter);
                flagFilterTextBox.Leave += new EventHandler(flagFilterTextBox_Leave);

                statusFilterTextBox.Text = ">Status";
                statusFilterTextBox.ForeColor = Color.Gray;
                statusFilterTextBox.Enter += new EventHandler(statusFilterTextBox_Enter);
                statusFilterTextBox.Leave += new EventHandler(statusFilterTextBox_Leave);

                cheskSystemResponsFilterTextBox.Text = ">Chesk System Respons";
                cheskSystemResponsFilterTextBox.ForeColor = Color.Gray;
                cheskSystemResponsFilterTextBox.Enter += new EventHandler(cheskSystemResponsFilterTextBox_Enter);
                cheskSystemResponsFilterTextBox.Leave += new EventHandler(cheskSystemResponsFilterTextBox_Leave);



                manualSubmitButton.MouseEnter += new EventHandler(manualSubmitButton_Enter);
                manualSubmitButton.MouseLeave += new EventHandler(manualSubmitButton_Leave);

                runTestButton.MouseEnter += new EventHandler(runTestButton_Enter);
                runTestButton.MouseLeave += new EventHandler(runTestButton_Leave);

                applyFilterButton.MouseEnter += new EventHandler(applyFilterButton_Enter);
                applyFilterButton.MouseLeave += new EventHandler(applyFilterButton_Leave);

                nextPageFlagsTableButton.MouseEnter += new EventHandler(nextPageFlagsTableButton_Enter);
                nextPageFlagsTableButton.MouseLeave += new EventHandler(nextPageFlagsTableButton_Leave);

                prevPageFlagsTableButton.MouseEnter += new EventHandler(prevPageFlagsTableButton_Enter);
                prevPageFlagsTableButton.MouseLeave += new EventHandler(prevPageFlagsTableButton_Leave);

                nextPageTeamsTableButton.MouseEnter += new EventHandler(nextPageTeamsTableButton_Enter);
                nextPageTeamsTableButton.MouseLeave += new EventHandler(nextPageTeamsTableButton_Leave);

                prevPageTeamsTableButton.MouseEnter += new EventHandler(prevPageTeamsTableButton_Enter);
                prevPageTeamsTableButton.MouseLeave += new EventHandler(prevPageTeamsTableButton_Leave);


                #region Pages of tables
                curPageFlagsTableTextBox.Text = "1";
                pagesOfMaxForFlagsPanelLabel.Text = "of 1";
                List<object>? flags = DBWorkForm.ReadClassFromDB_AllClass(new FlagHistory());
                if (flags != null)
                {
                    if (flags.Count % 13 == 0)
                    {
                        pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((flags.Count - (flags.Count % 13)) / 13));
                    }
                    else
                    {
                        pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((flags.Count - (flags.Count % 13)) / 13) + 1);
                    }
                }

                curPageTeamsTableTextBox.Text = "1";
                pagesOfMaxForTeamsPanelLabel.Text = "of 1";
                #endregion


                setFlagStatusGridView();
                setTeamsPlaceDataGridView();

                if (onOffAutoTeamParseToolStripMenuItem.Checked)
                {
                    PythonGetRequest(false);
                }

                List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
                if (ctfTeams != null)
                {
                    if (ctfTeams.Count % 11 == 0)
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfTeams.Count - (ctfTeams.Count % 11)) / 11);
                    }
                    else
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfTeams.Count - (ctfTeams.Count % 11)) / 11) + 1);
                    }
                    teamChooseComboBox.Items.Add("All teams");
                    foreach (var teams in ctfTeams) //создаёт постоянную с IP команд и их именем
                    {
                        CTFTeam team = teams as CTFTeam;
                        if (team.teamIP != fs.teamOwnerIP)
                        {
                            teamsList.Add(new KeyValuePair<string, string>(team.teamIP, team.teamName));
                            teamChooseComboBox.Items.Add(team.teamName);
                        }
                    }
                }

                #region Threads Start
                TeamsUpdateTHR = new Thread(teamUpdate);
                TeamsUpdateTHR.Start();

                FlagsUpdateTHR = new Thread(flagsUpdate);
                FlagsUpdateTHR.Start();

                AutoSploitRunTHR = new Thread(autoSploitRun);
                AutoSploitRunTHR.Start();

                SploitDirectoryCheckTHR = new Thread(sploitDirectoryCheck);
                SploitDirectoryCheckTHR.Start();

                FlagsSendTHR = new Thread(flagsSend);
                FlagsSendTHR.Start();

                FailSafeTHR = new Thread(failSafe);
                FailSafeTHR.Start();
                #endregion

                this.FormClosed += new FormClosedEventHandler(MainForm_Closed);
                this.Text = "VQT Farm (Online)";
            }
            catch (Exception exp)
            {
                if (!errorsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Error!\nSome problem while loadin farm\n{exp}", "ERROR");
                }
                StopThreadings();
                this.Close();
            }
        }

        private void MainForm_Closed(object? sender, FormClosedEventArgs e)
        {
            StopThreadings();
        }

        #region Seters for GridView
        private void setFlagStatusGridView()
        {
            try
            {
                flagStatusGridView.Columns.Add("exsploit_name", "Exploit");
                flagStatusGridView.Columns.Add("team_name", "Team");
                flagStatusGridView.Columns.Add("sended_flag", "Flag");
                flagStatusGridView.Columns.Add("time", "Time");
                flagStatusGridView.Columns.Add("status", "Status");
                flagStatusGridView.Columns.Add("cheskSystemRespons", "Chesk System Respons");


                flagStatusGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                flagStatusGridView.RowHeadersVisible = false;

                flagStatusGridView.Columns[0].Width = 150;
                flagStatusGridView.Columns[1].Width = 150;
                flagStatusGridView.Columns[2].Width = 300;
                flagStatusGridView.Columns[3].Width = 200;
                flagStatusGridView.Columns[4].Width = 100;
                flagStatusGridView.Columns[5].Width = 300;

                //flagStatusGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                flagStatusGridView.Width = flagStatusGridView.Columns[0].Width +
                    flagStatusGridView.Columns[1].Width +
                    flagStatusGridView.Columns[2].Width +
                    flagStatusGridView.Columns[3].Width +
                    flagStatusGridView.Columns[4].Width +
                    flagStatusGridView.Columns[5].Width + 20;

                flagStatusGridView.ClearSelection();
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with set settings for Flag Status DataGridView\n{exp}", "WARNING");
                }
            }
        }
        private void setTeamsPlaceDataGridView()
        {
            try
            {
                teamsPlaceDataGridView.Columns.Add("team_place", "Place");
                teamsPlaceDataGridView.Columns.Add("team_name", "Team");
                teamsPlaceDataGridView.Columns.Add("team_ip", "IP");
                teamsPlaceDataGridView.Columns.Add("team_score", "Score");

                teamsPlaceDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                teamsPlaceDataGridView.RowHeadersVisible = false;

                teamsPlaceDataGridView.Columns[0].Width = 100;
                teamsPlaceDataGridView.Columns[1].Width = 150;
                teamsPlaceDataGridView.Columns[2].Width = 100;
                teamsPlaceDataGridView.Columns[3].Width = 150;

                //teamsPlaceDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                teamsPlaceDataGridView.Width = teamsPlaceDataGridView.Columns[0].Width +
                    teamsPlaceDataGridView.Columns[1].Width +
                    teamsPlaceDataGridView.Columns[2].Width +
                    teamsPlaceDataGridView.Columns[3].Width + 20;

                teamsPlaceDataGridView.ClearSelection();
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with set settings for Teams Place DataGridView\n{exp}", "WARNING");
                }
            }
        }
        #endregion


        #region Threadusable functions
        private bool PythonConnectionCheck()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = "python.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\"", pathToConnectionCheckScript, fs.flagSubmitterURL),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };
                string answ = "";
                Process conCheck = Process.Start(start);
                using (StreamReader reader = conCheck.StandardOutput)
                {
                    int i = 0;
                    while (!conCheck.HasExited)
                    {
                        conCheck.WaitForExit(20);
                        i++;
                        if (i * 20 >= fs.roundTime)
                        {
                            answ = reader.ReadToEnd();
                            conCheck.Kill();
                            if (!informationToolStripMenuItem.Checked)
                            {
                                MessageBox.Show("The \"Connection check\" process is killed", "TimeOut Exception");
                            }
                            break;
                        }
                    }
                    if (answ == "")
                    {
                        answ = reader.ReadToEnd();
                    }
                }
                if (answ.Contains("True"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with connection check script\n{exp}", "WARNING");
                }
                return false;
            }
        }
        private void PythonFlagSendRequest(string[] flags, string teamName, string exploitName)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = "python.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", fs.pythonFlagSendScriptPath, fs.flagSubmitterURL, JsonSerializer.Serialize(flags), fs.teamToken, teamName, exploitName),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };
                Process flagSendProc = Process.Start(start);
                for (int i = 0; i <= 500; i++)
                {
                    if (flagSendProc.HasExited)
                    {
                        return;
                    }
                    else
                    {
                        if (flagSendProc.WaitForExit(20))
                            return;
                        else
                            continue;
                    }
                }
                flagSendProc.Kill();
                if (!informationToolStripMenuItem.Checked)
                {
                    MessageBox.Show("The \"PythonFlagSendRequest\" process is killed", "TimeOut Exception");
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with flag sending script\n{exp}", "WARNING");
                }
            }
        }
        private void PythonGetRequest(bool isUpdate)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = "python.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\"", fs.pythonGetScriptPath, fs.scoreBoardURL, isUpdate == true ? "1" : "0"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };
                Process getProc = Process.Start(start);
                for (int i = 0; i * 20 < fs.roundTime; i++)
                {
                    if (getProc.HasExited)
                    {
                        return;
                    }
                    else
                    {
                        getProc.WaitForExit(20);
                    }
                }
                getProc.Kill();
                if (!informationToolStripMenuItem.Checked)
                {
                    MessageBox.Show("The \"PythonGetRequest\" process is killed", "TimeOut Exception");
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with parsing script\n{exp}", "WARNING");
                }
            }
        }
        private void runScript(object? obj)
        {
            try
            {
                ThreadInfoClass thrInfo = obj as ThreadInfoClass;
                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = "python.exe",
                    Arguments = string.Format("\"{0}\" \"{1}\"", thrInfo.scriptPath, thrInfo.ip),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                };
                string answ = "";

                Process process = Process.Start(start);
                using (StreamReader reader = process.StandardOutput)
                {
                    int i = 0;
                    while (!process.HasExited)
                    {
                        process.WaitForExit(20);
                        i++;
                        if (i * 20 >= fs.roundTime)
                        {
                            answ = reader.ReadToEnd();
                            process.Kill();
                            if (!informationToolStripMenuItem.Checked)
                            {
                                MessageBox.Show("The \"Sploit\" process is killed", "TimeOut Exception");
                            }
                            break;
                        }
                    }
                    if (answ == "")
                    {
                        answ = reader.ReadToEnd();
                    }
                }

                var m1 = Regex.Matches(answ, fs.flagFormat);
                var flags = new List<string>();
                foreach (Match match in m1)
                {
                    flags.Add(match.Value);
                }
                thrInfo.flags = flags.ToArray();
                if (thrInfo.flags.Length <= 0)
                {
                    return;
                }
                flagsQueue.Enqueue(new KeyValuePair<ThreadInfoClass, DateTime>(thrInfo, DateTime.Now));
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem with script running\n{exp}", "WARNING");
                }
            }
        }
        #endregion
        #region Threading Events
        private void StopThreadings()
        {
            try
            {
                isFailSafeTHRMustRun = false;
                Thread.Sleep(500);
                isTeamsUpdateTHRMustRun = false;
                isFlagsUpdateTHRMustRun = false;
                isSploitDirectoryCheckTHRMustRun = false;
                isAutoSploitRunTHRMustRun = false;
                isFlagsSendTHRMustRun = false;
            }
            catch (Exception exp)
            {
                if (!errorsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Error!\nSome problem in stopping threadings.\nPlease, close threadings by using Task Manager and restart Farm\n{exp}", "ERROR");
                }
            }
        }
        private void failSafe()
        {
            try
            {
                while (isFailSafeTHRMustRun)
                {
                    Thread.Sleep(FailSafeTHRCoolDown);
                    if (isFailSafeTHRMustRun)
                    {
                        if (TeamsUpdateTHR.ThreadState != System.Threading.ThreadState.Running && TeamsUpdateTHR.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                        {
                            if (teamsListPanel.Visible == true)
                            {
                                isTeamsUpdateTHRMustRun = true;
                                TeamsUpdateTHR = new Thread(teamUpdate);
                                TeamsUpdateTHR.Start();
                            }
                        }
                        if (FlagsUpdateTHR.ThreadState != System.Threading.ThreadState.Running && FlagsUpdateTHR.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                        {
                            if (flagStatusPanel.Visible == true)
                            {
                                isFlagsUpdateTHRMustRun = true;
                                FlagsUpdateTHR = new Thread(flagsUpdate);
                                FlagsUpdateTHR.Start();
                            }
                        }
                        if (SploitDirectoryCheckTHR.ThreadState != System.Threading.ThreadState.Running && SploitDirectoryCheckTHR.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                        {
                            isSploitDirectoryCheckTHRMustRun = true;
                            SploitDirectoryCheckTHR = new Thread(sploitDirectoryCheck);
                            SploitDirectoryCheckTHR.Start();
                        }
                        if (AutoSploitRunTHR.ThreadState != System.Threading.ThreadState.Running && AutoSploitRunTHR.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                        {
                            isAutoSploitRunTHRMustRun = true;
                            AutoSploitRunTHR = new Thread(autoSploitRun);
                            AutoSploitRunTHR.Start();
                        }
                        if (FlagsSendTHR.ThreadState != System.Threading.ThreadState.Running && FlagsSendTHR.ThreadState != System.Threading.ThreadState.WaitSleepJoin)
                        {
                            isFlagsSendTHRMustRun = true;
                            FlagsSendTHR = new Thread(flagsSend);
                            FlagsSendTHR.Start();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                if (!errorsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Error!\nSome problem in restart multiprocessing.\nPlease, restart farm for correct work!\n{exp}", "ERROR");
                }
            }
        }
        private void flagsSend()
        {
            try
            {
                while (isFlagsSendTHRMustRun)
                {
                    if (onOffConectionCheckToolStripMenuItem.Checked && flagsQueue.Count > 0)
                    {
                        if (!PythonConnectionCheck())
                        {
                            if (!informationToolStripMenuItem.Checked)
                            {
                                MessageBox.Show("The flag delivery service is not available");
                            }
                            Thread.Sleep(fs.roundTime);
                            continue;
                        }
                    }

                    var isQueueContainsFlags = flagsQueue.TryDequeue(out KeyValuePair<ThreadInfoClass, DateTime> thrInfo);
                    if (isQueueContainsFlags)
                    {
                        if (thrInfo.Value.AddMilliseconds(fs.roundTime * fs.flagLifeTime) >= DateTime.Now)
                        {
#if DebagTestFunction                                                                                       //////////////////////////////////////////////////////////
                            string path = "";
                            try
                            {
                                path = Path.GetFileName(thrInfo.Key.scriptPath);
                            }
                            catch
                            {
                                path = thrInfo.Key.scriptPath;
                            }

                            PythonFlagSendRequest(thrInfo.Key.flags, thrInfo.Key.team_name, path);
#else
                            PythonFlagSendRequest(thrInfo.Key.flags, thrInfo.Key.team_name, Path.GetFileName(thrInfo.Key.scriptPath));
#endif
                        }
                        else
                        {
                            continue;
                        }
                    }
                    Thread.Sleep(FlagsSendTHRCoolDown);
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while flag sending\n{exp}", "WARNING");
                }
            }
        }
        private void teamUpdate()
        {
            try
            {
                DataBaseWorkAplication DBWorkFormTeam = new DataBaseWorkAplication();
                DBWorkFormTeam.StartConnection("Data Source=FarmInfo.db");
                while (isTeamsUpdateTHRMustRun)
                {
                    if (onOffAutoTeamParseToolStripMenuItem.Checked)
                    {
                        PythonGetRequest(true);
                    }
                    List<object>? ctfteams = DBWorkFormTeam.ReadClassFromDB_AllClass(new CTFTeam());
                    nextPageTeamsTableButton.Enabled = false;
                    prevPageTeamsTableButton.Enabled = false;
                    if (ctfteams != null)
                    {
                        if (ctfteams.Count % 11 == 0)
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfteams.Count - (ctfteams.Count % 11)) / 11);
                        }
                        else
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfteams.Count - (ctfteams.Count % 11)) / 11) + 1);
                        }
                        teamsPlaceDataGridView.Rows.Clear();

                        int i = 0;
                        for (int j = (Convert.ToInt32(curPageTeamsTableTextBox.Text) - 1) * 11; j < ctfteams.Count; j++)
                        {
                            if (i >= 11)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            CTFTeam team = ctfteams[j] as CTFTeam;

                            teamsPlaceDataGridView.Rows.Add();
                            if (team.GetType().GetField("teamPlace") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = team.teamPlace;
                                teamsPlaceDataGridView[0, i] = textBoxCell1;
                            }

                            if (team.GetType().GetField("teamName") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = team.teamName;
                                teamsPlaceDataGridView[1, i] = textBoxCell2;
                            }

                            if (team.GetType().GetField("teamIP") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = team.teamIP;
                                teamsPlaceDataGridView[2, i] = textBoxCell3;
                            }

                            if (team.GetType().GetField("teamScore") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = team.teamScore;
                                teamsPlaceDataGridView[3, i] = textBoxCell4;
                            }

                            i++;
                        }
                        teamsPlaceDataGridView.Sort(teamsPlaceDataGridView.Columns["team_place"], System.ComponentModel.ListSortDirection.Ascending);
                    }
                    nextPageTeamsTableButton.Enabled = true;
                    prevPageTeamsTableButton.Enabled = true;
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while updating teams info\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageTeamsTableButton.Enabled = true;
                prevPageTeamsTableButton.Enabled = true;
            }
        }
        private void autoSploitRun()
        {
            try
            {
                while (isAutoSploitRunTHRMustRun)
                {
                    foreach (var team in teamsList)
                    {
                        for (int i = 0; i < sploitsList.Count; i++)
                        {
                            Thread thread = new Thread(runScript);
                            thread.Start(new ThreadInfoClass(sploitsList[i], team.Key, team.Value));
                        }
                    }
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while auto run sploits\n{exp}", "WARNING");
                }
            }
        }
        private void flagsUpdate()
        {
            try
            {
                DataBaseWorkAplication DBWorkFormFlag = new DataBaseWorkAplication();
                DBWorkFormFlag.StartConnection("Data Source=FarmInfo.db");
                while (isFlagsUpdateTHRMustRun)
                {
                    List<object>? SendedFlags = DBWorkFormFlag.ReadClassFromDB_AllClass(new FlagHistory());
                    List<object>? AcceptedFlags = DBWorkFormFlag.ReadClassFromDB_AllClass_byParams(new FlagHistory(), new List<string>() { "status='ACCEPTED'" });
                    if (SendedFlags != null)
                    {
                        flagTotalSendedLabel.Text = $"TOTAL FLAGS SENDED: {SendedFlags.Count}";
                        if (AcceptedFlags != null)
                        {
                            flagTotalAceptedLabel.Text = $"TOTAL FLAGS ACCEPTED: {AcceptedFlags.Count}";
                        }
                    }

                    List<object>? AllFlags = new List<object>();
                    if (filtersForFlagShow.Count > 0)
                    {
                        AllFlags = DBWorkFormFlag.ReadClassFromDB_AllClass_byParams(new FlagHistory(), filtersForFlagShow);
                    }
                    else
                    {
                        AllFlags = SendedFlags;
                    }

                    nextPageFlagsTableButton.Enabled = false;
                    prevPageFlagsTableButton.Enabled = false;

                    if (AllFlags != null)
                    {
                        if (AllFlags.Count % 13 == 0)
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13));
                        }
                        else
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13) + 1);
                        }
                        flagStatusGridView.Rows.Clear();

                        int j = 0;

                        for (int i = (AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13 < 0) ? AllFlags.Count - 1 : AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13; i >= 0; i--)
                        {
                            if (j >= 13)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            FlagHistory flag = AllFlags[i] as FlagHistory;

                            flagStatusGridView.Rows.Add();

                            if (flag.GetType().GetField("sploit_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = flag.sploit_name;
                                flagStatusGridView[0, j] = textBoxCell1;
                            }
                            if (flag.GetType().GetField("team_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = flag.team_name;
                                flagStatusGridView[1, j] = textBoxCell2;
                            }
                            if (flag.GetType().GetField("sended_flag") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = flag.sended_flag;
                                flagStatusGridView[2, j] = textBoxCell3;
                            }
                            if (flag.GetType().GetField("time") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = flag.time;
                                flagStatusGridView[3, j] = textBoxCell4;
                            }
                            if (flag.GetType().GetField("status") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell5 = new DataGridViewTextBoxCell();
                                textBoxCell5.Value = flag.status;
                                flagStatusGridView[4, j] = textBoxCell5;
                            }
                            if (flag.GetType().GetField("cheskSystemRespons") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell6 = new DataGridViewTextBoxCell();
                                textBoxCell6.Value = flag.cheskSystemRespons;
                                flagStatusGridView[5, j] = textBoxCell6;
                            }

                            j++;
                        }
                    }
                    nextPageFlagsTableButton.Enabled = true;
                    prevPageFlagsTableButton.Enabled = true;
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while updating flag history\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Enabled = true;
            }
        }
        private void sploitDirectoryCheck()
        {
            try
            {
                while (isSploitDirectoryCheckTHRMustRun)
                {
                    if (!Directory.Exists("Sploits"))
                    {
                        Directory.CreateDirectory("Sploits");
                    }
                    var sploits = Directory.GetFiles("Sploits");

                    if (sploits.Length > 0)
                    {
                        foreach (var item in sploits)
                        {
                            if (!sploitsList.Contains(item))
                            {
                                sploitsList.Clear();
                                foreach (var sploit in sploits)
                                {
                                    sploitsList.Add(sploit);
                                }
                            }
                        }
                    }
                    else
                    {
                        sploitsList.Clear();
                    }
                    Thread.Sleep(SploitDirectoryCheckTHRCoolDown);
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while reading sploits from directory\n{exp}", "WARNING");
                }
            }
        }
#endregion


        #region Menu Strip
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        #region Settings Menu
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Add Team Manual Menu
        private void addTeamManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManualTeamAddForm teamAddForm = new ManualTeamAddForm();
            teamAddForm.FormClosed += new FormClosedEventHandler(teamAddForm_Closed);
            teamAddForm.ShowDialog();
        }
        private void teamAddForm_Closed(object? sender, FormClosedEventArgs e)
        {
            try
            {
                List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
                if (ctfTeams != null)
                {
                    if (ctfTeams.Count % 11 == 0)
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfTeams.Count - (ctfTeams.Count % 11)) / 11);
                    }
                    else
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfTeams.Count - (ctfTeams.Count % 11)) / 11) + 1);
                    }
                    teamsList.Clear();
                    teamChooseComboBox.Items.Clear();
                    teamChooseComboBox.Items.Add("-Select Team for test-");
                    teamChooseComboBox.Items.Add("All teams");
                    teamChooseComboBox.SelectedItem = "-Select Team for test-";
                    foreach (var teams in ctfTeams) //создаёт постоянную с IP команд и их именем
                    {
                        CTFTeam team = teams as CTFTeam;
                        if (team.teamIP != fs.teamOwnerIP)
                        {
                            teamsList.Add(new KeyValuePair<string, string>(team.teamIP, team.teamName));
                            teamChooseComboBox.Items.Add(team.teamName);
                        }
                    }
                }
                else
                {
                    teamsList.Clear();
                    teamChooseComboBox.Items.Clear();
                    teamChooseComboBox.Items.Add("-Select Team for test-");
                    teamChooseComboBox.SelectedItem = "-Select Team for test-";
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while updating teams after manual add\n{exp}", "WARNING");
                }
            }
        }

        #endregion

        #region Show Menu
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void teamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (teamsToolStripMenuItem.Checked)
            {
                teamsToolStripMenuItem.CheckState = CheckState.Unchecked;
                teamsListPanel.Visible = false;
                isTeamsUpdateTHRMustRun = false;
            }
            else
            {
                teamsToolStripMenuItem.CheckState = CheckState.Checked;
                teamsListPanel.Visible = true;
            }
        }
        private void flagHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagHistoryToolStripMenuItem.Checked)
            {
                flagHistoryToolStripMenuItem.CheckState = CheckState.Unchecked;
                flagStatusPanel.Visible = false;
                isFlagsUpdateTHRMustRun = false;
            }
            else
            {
                flagHistoryToolStripMenuItem.CheckState = CheckState.Checked;
                flagStatusPanel.Visible = true;
            }
        }
        private void manualSubmitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (manualSubmitToolStripMenuItem.Checked)
            {
                manualSubmitToolStripMenuItem.CheckState = CheckState.Unchecked;
                manualSubmitPanel.Visible = false;
            }
            else
            {
                manualSubmitToolStripMenuItem.CheckState = CheckState.Checked;
                manualSubmitPanel.Visible = true;
            }
        }
        private void exploitTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (exploitTestToolStripMenuItem.Checked)
            {
                exploitTestToolStripMenuItem.CheckState = CheckState.Unchecked;
                exploitTestPanel.Visible = false;
            }
            else
            {
                exploitTestToolStripMenuItem.CheckState = CheckState.Checked;
                exploitTestPanel.Visible = true;
            }
        }
        private void flagShowFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flagShowFilterToolStripMenuItem.Checked)
            {
                flagShowFilterToolStripMenuItem.CheckState = CheckState.Unchecked;
                flagShowFilterPanel.Visible = false;
            }
            else
            {
                flagShowFilterToolStripMenuItem.CheckState = CheckState.Checked;
                flagShowFilterPanel.Visible = true;
            }
        }

        #endregion

        #region On/Off Farm Menu
        private void startStopFarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startStopFarmToolStripMenuItem.Enabled = false;
            if (Text == "VQT Farm (Online)")
            {
                Text = "VQT Farm (Offline)";
                StopThreadings();
            }
            else if (Text == "VQT Farm (Offline)")
            {
                Text = "VQT Farm (Online)";
                isFailSafeTHRMustRun = true;
                FailSafeTHR = new Thread(failSafe);
                FailSafeTHR.Start();
            }
            Thread.Sleep(2000);
            startStopFarmToolStripMenuItem.Enabled = true;
        }
        #endregion

        #region On/Off Auto Team Parse Menu
        private void onOffAutoTeamParseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (onOffAutoTeamParseToolStripMenuItem.Checked == true)
            {
                onOffAutoTeamParseToolStripMenuItem.Checked = false;
                onOffAutoTeamParseToolStripMenuItem.Text = "On Auto team parse (OFF)";
                onOffAutoTeamParseToolStripMenuItem.ForeColor = Color.Red;
            }
            else if (onOffAutoTeamParseToolStripMenuItem.Checked == false)
            {
                onOffAutoTeamParseToolStripMenuItem.Checked = true;
                onOffAutoTeamParseToolStripMenuItem.Text = "Off Auto team parse (ON)";
                onOffAutoTeamParseToolStripMenuItem.ForeColor = Color.Green;
            }
        }
        #endregion

        #region On/Off Conection check Menu
        private void onOffConectionCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (onOffConectionCheckToolStripMenuItem.Checked == true)
            {
                onOffConectionCheckToolStripMenuItem.Checked = false;
                onOffConectionCheckToolStripMenuItem.Text = "On Conection check (OFF)";
                onOffConectionCheckToolStripMenuItem.ForeColor = Color.Red;
            }
            else if (onOffConectionCheckToolStripMenuItem.Checked == false)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Python files(*.py)|*.py";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = "Choose conection check script";

                if (!(openFileDialog.ShowDialog() == DialogResult.Cancel))
                {
                    pathToConnectionCheckScript = openFileDialog.FileName;
                    onOffConectionCheckToolStripMenuItem.Checked = true;
                    onOffConectionCheckToolStripMenuItem.Text = "Off Conection check (ON)";
                    onOffConectionCheckToolStripMenuItem.ForeColor = Color.Green;
                }
            }
        }
        #endregion

        #region On/Off Pop-Up Messanges
        private void disablePopUpMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (informationToolStripMenuItem.Checked == true)
            {
                informationToolStripMenuItem.Checked = false;
            }
            else if (informationToolStripMenuItem.Checked == false)
            {
                informationToolStripMenuItem.Checked = true;
            }
        }
        private void warningsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (warningsToolStripMenuItem.Checked == true)
            {
                warningsToolStripMenuItem.Checked = false;
            }
            else if (warningsToolStripMenuItem.Checked == false)
            {
                var answ = MessageBox.Show("Warning messages let you know that something has happened and the program has worked incorrectly, although it has corrected the error.\nDo you really want to disable these messages?", "Attention!", MessageBoxButtons.YesNo);
                if (answ == DialogResult.Yes)
                {
                    warningsToolStripMenuItem.Checked = true;
                }
            }
        }
        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (errorsToolStripMenuItem.Checked == true)
            {
                errorsToolStripMenuItem.Checked = false;
            }
            else if (errorsToolStripMenuItem.Checked == false)
            {
                var answ = MessageBox.Show("Error messages let you know that the program is broken.\nDo you really want to disable these messages?", "Attention!", MessageBoxButtons.YesNo);
                if (answ == DialogResult.Yes)
                {
                    errorsToolStripMenuItem.Checked = true;
                }
            }
        }
        #endregion

        #region Fix Tables Menu
        private void fixTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopThreadings();
            this.Controls.Clear();
            InitializeComponent();
            this.Text = "VQT Farm (Starting)";
            teamsToolStripMenuItem.CheckState = CheckState.Checked;
            flagHistoryToolStripMenuItem.CheckState = CheckState.Checked;
            manualSubmitToolStripMenuItem.CheckState = CheckState.Checked;
            exploitTestToolStripMenuItem.CheckState = CheckState.Checked;
            flagShowFilterToolStripMenuItem.CheckState = CheckState.Checked;


            exploitChooseTextBox.Text = "-Choose Exploit for test-";

            teamChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            teamChooseComboBox.Items.Add("-Select Team for test-");
            teamChooseComboBox.SelectedItem = "-Select Team for test-";

            exploitChooseTextBox.Click += new EventHandler(exploitChooseTextBox_Click);
            exploitChooseTextBox.Enter += new EventHandler(exploitChooseTextBox_Enter);
            exploitChooseTextBox.Cursor = Cursors.Arrow;


            exploitFilterTextBox.Text = ">Exploit Name";
            exploitFilterTextBox.ForeColor = Color.Gray;
            exploitFilterTextBox.Enter += new EventHandler(exploitFilterTextBox_Enter);
            exploitFilterTextBox.Leave += new EventHandler(exploitFilterTextBox_Leave);

            teamFilterTextBox.Text = ">Team Name";
            teamFilterTextBox.ForeColor = Color.Gray;
            teamFilterTextBox.Enter += new EventHandler(teamFilterTextBox_Enter);
            teamFilterTextBox.Leave += new EventHandler(teamFilterTextBox_Leave);

            flagFilterTextBox.Text = ">Flag";
            flagFilterTextBox.ForeColor = Color.Gray;
            flagFilterTextBox.Enter += new EventHandler(flagFilterTextBox_Enter);
            flagFilterTextBox.Leave += new EventHandler(flagFilterTextBox_Leave);

            statusFilterTextBox.Text = ">Status";
            statusFilterTextBox.ForeColor = Color.Gray;
            statusFilterTextBox.Enter += new EventHandler(statusFilterTextBox_Enter);
            statusFilterTextBox.Leave += new EventHandler(statusFilterTextBox_Leave);

            cheskSystemResponsFilterTextBox.Text = ">Chesk System Respons";
            cheskSystemResponsFilterTextBox.ForeColor = Color.Gray;
            cheskSystemResponsFilterTextBox.Enter += new EventHandler(cheskSystemResponsFilterTextBox_Enter);
            cheskSystemResponsFilterTextBox.Leave += new EventHandler(cheskSystemResponsFilterTextBox_Leave);


            #region Pages of tables
            curPageFlagsTableTextBox.Text = "1";
            pagesOfMaxForFlagsPanelLabel.Text = "of 1";
            List<object>? flags = DBWorkForm.ReadClassFromDB_AllClass(new FlagHistory());
            if (flags != null)
            {
                if (flags.Count % 13 == 0)
                {
                    pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((flags.Count - (flags.Count % 13)) / 13));
                }
                else
                {
                    pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((flags.Count - (flags.Count % 13)) / 13) + 1);
                }
            }

            curPageTeamsTableTextBox.Text = "1";
            pagesOfMaxForTeamsPanelLabel.Text = "of 1";
            #endregion



            setFlagStatusGridView();
            setTeamsPlaceDataGridView();


            List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
            if (ctfTeams != null)
            {
                if (ctfTeams.Count % 11 == 0)
                {
                    pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfTeams.Count - (ctfTeams.Count % 11)) / 11);
                }
                else
                {
                    pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfTeams.Count - (ctfTeams.Count % 11)) / 11) + 1);
                }
                teamChooseComboBox.Items.Add("All teams");
                foreach (var teams in ctfTeams) //создаёт постоянную с IP команд и их именем
                {
                    CTFTeam team = teams as CTFTeam;
                    if (team.teamIP != fs.teamOwnerIP)
                    {
                        teamChooseComboBox.Items.Add(team.teamName);
                    }
                }
            }

            this.FormClosed += new FormClosedEventHandler(MainForm_Closed);
            this.Text = "VQT Farm (Online)";

            isFailSafeTHRMustRun = true;
            FailSafeTHR = new Thread(failSafe);
            FailSafeTHR.Start();
        }
        #endregion

        #region Auto teams generation Menu
        private void autoTeamsGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoTeamsGenerationForm teamGenerationForm = new AutoTeamsGenerationForm();
            teamGenerationForm.FormClosed += new FormClosedEventHandler(teamGenerationForm_Closed);
            teamGenerationForm.ShowDialog();
        }
        private void teamGenerationForm_Closed(object? sender, FormClosedEventArgs e)
        {
            try
            {
                List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
                if (ctfTeams != null)
                {
                    if (ctfTeams.Count % 11 == 0)
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfTeams.Count - (ctfTeams.Count % 11)) / 11);
                    }
                    else
                    {
                        pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfTeams.Count - (ctfTeams.Count % 11)) / 11) + 1);
                    }
                    teamsList.Clear();
                    teamChooseComboBox.Items.Clear();
                    teamChooseComboBox.Items.Add("-Select Team for test-");
                    teamChooseComboBox.Items.Add("All teams");
                    teamChooseComboBox.SelectedItem = "-Select Team for test-";
                    foreach (var teams in ctfTeams) //создаёт постоянную с IP команд и их именем
                    {
                        CTFTeam team = teams as CTFTeam;
                        if (team.teamIP != fs.teamOwnerIP)
                        {
                            teamsList.Add(new KeyValuePair<string, string>(team.teamIP, team.teamName));
                            teamChooseComboBox.Items.Add(team.teamName);
                        }
                    }
                }
                else
                {
                    teamsList.Clear();
                    teamChooseComboBox.Items.Clear();
                    teamChooseComboBox.Items.Add("-Select Team for test-");
                    teamChooseComboBox.SelectedItem = "-Select Team for test-";
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while updating teams after auto generated teams add\n{exp}", "WARNING");
                }
            }
        }
        #endregion

        #endregion

        #region Help Menu
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In the main window of the program, you can manage and configure the necessary work parameters:\n\n" +
                "1)Settings - farm settings:\n" +
                "   a)Add team manual - manually adding a team to the list of attacked teams with specifying their IP and name.\n" +
                "   b)Auto teams generation - automatic generation of commands in a given range of IP addresses to the list of attacked commands with the assignment of their IP and default name.\n" +
                "   c)Start/Stop Farm - enabling/disabling program threads. Stops the farm by disabling sending flags and updating tables.\n" +
                "   d)On Auto team parse (OFF) - enabling the function of automatic team parsing from scoreboards, subject to the availability of a working script. Updates team data once a round: their position, name, IP and flagpoints.(the working script should be selected on the initial window, being added to the folder with the corresponding scripts)\n" +
                "   e)On Conection check (OFF) - enables the function of checking the availability of the check system. After clicking, you need to select the python script to check.\n" +
                "   f)Show - enables/disables elements of the working window. In addition to visually disabling them, it stops the work thread associated with this element.\n" +
                "   g)Disable pop-up messages - enables/disables the display of notification messages, warnings and errors.\n" +
                "   h)Fix Tables - fix a bug with tables when they stop displaying information.\n" +
                "2)Help - shows information from this block.\n" +
                "3)Manual Submit - panel for manually sending flags.\n" +
                "4)Sploit test - testing the operability of the sploits before they are added to the main process, with the choice of the attacked team.\n" +
                "5)Teams - a table showing the teams (their position, name, IP, flag points). Pagination is available.\n" +
                "6)Flag status - a table showing the sent flags (by which unit it was received, from which command, which flag, the time of sending, the status and the response from the check system). Also shows the total number of sent flags and accepted flags. Pagination is available.\n" +
                "7)Flag show filter - a panel with filter settings for displaying information on flags in the table from the previous paragraph.", "Help", MessageBoxButtons.OK);
        }

        #endregion

        #endregion


        #region Flag Status Panel
        private void flagStatusGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void flagStatusLabel_Click(object sender, EventArgs e)
        {

        }
        private void flagTotalSendedLabel_Click(object sender, EventArgs e)
        {

        }
        private void flagTotalAceptedLabel_Click(object sender, EventArgs e)
        {

        }
        private void flagStatusPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void prevPageFlagsTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                string buf = curPageFlagsTableTextBox.Text;
                curPageFlagsTableTextBox.Text = Convert.ToInt32(curPageFlagsTableTextBox.Text) > 1 ? Convert.ToString(Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) : Convert.ToString(curPageFlagsTableTextBox.Text);
                if (buf != curPageFlagsTableTextBox.Text)
                {
                    DataBaseWorkAplication DBWorkFormFlagFilter = new DataBaseWorkAplication();
                    DBWorkFormFlagFilter.StartConnection("Data Source=FarmInfo.db");
                    List<object>? AllFlags = new List<object>();
                    if (filtersForFlagShow.Count > 0)
                    {
                        AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass_byParams(new FlagHistory(), filtersForFlagShow);
                    }
                    else
                    {
                        AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass(new FlagHistory()); ;
                    }


                    nextPageFlagsTableButton.Enabled = false;
                    prevPageFlagsTableButton.Enabled = false;

                    flagStatusGridView.Rows.Clear();
                    if (AllFlags != null)
                    {
                        if (AllFlags.Count % 13 == 0)
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13));
                        }
                        else
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13) + 1);
                        }
                        int j = 0;

                        for (int i = (AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13 < 0) ? AllFlags.Count - 1 : AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13; i >= 0; i--)
                        {
                            if (j >= 13)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            FlagHistory flag = AllFlags[i] as FlagHistory;

                            flagStatusGridView.Rows.Add();

                            if (flag.GetType().GetField("sploit_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = flag.sploit_name;
                                flagStatusGridView[0, j] = textBoxCell1;
                            }
                            if (flag.GetType().GetField("team_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = flag.team_name;
                                flagStatusGridView[1, j] = textBoxCell2;
                            }
                            if (flag.GetType().GetField("sended_flag") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = flag.sended_flag;
                                flagStatusGridView[2, j] = textBoxCell3;
                            }
                            if (flag.GetType().GetField("time") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = flag.time;
                                flagStatusGridView[3, j] = textBoxCell4;
                            }
                            if (flag.GetType().GetField("status") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell5 = new DataGridViewTextBoxCell();
                                textBoxCell5.Value = flag.status;
                                flagStatusGridView[4, j] = textBoxCell5;
                            }
                            if (flag.GetType().GetField("cheskSystemRespons") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell6 = new DataGridViewTextBoxCell();
                                textBoxCell6.Value = flag.cheskSystemRespons;
                                flagStatusGridView[5, j] = textBoxCell6;
                            }

                            j++;
                        }
                    }
                    nextPageFlagsTableButton.Enabled = true;
                    prevPageFlagsTableButton.Enabled = true;
                    prevPageFlagsTableButton.Select();
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while swithing flags pages\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Select();
            }
        }
        private void nextPageFlagsTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                string buf = curPageFlagsTableTextBox.Text;
                curPageFlagsTableTextBox.Text = Convert.ToInt32(curPageFlagsTableTextBox.Text) < Convert.ToInt32(pagesOfMaxForFlagsPanelLabel.Text.Split(' ')[1]) ? Convert.ToString(Convert.ToInt32(curPageFlagsTableTextBox.Text) + 1) : Convert.ToString(curPageFlagsTableTextBox.Text);
                if (buf != curPageFlagsTableTextBox.Text)
                {
                    DataBaseWorkAplication DBWorkFormFlagFilter = new DataBaseWorkAplication();
                    DBWorkFormFlagFilter.StartConnection("Data Source=FarmInfo.db");
                    List<object>? AllFlags = new List<object>();
                    if (filtersForFlagShow.Count > 0)
                    {
                        AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass_byParams(new FlagHistory(), filtersForFlagShow);
                    }
                    else
                    {
                        AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass(new FlagHistory()); ;
                    }


                    nextPageFlagsTableButton.Enabled = false;
                    prevPageFlagsTableButton.Enabled = false;

                    flagStatusGridView.Rows.Clear();
                    if (AllFlags != null)
                    {
                        if (AllFlags.Count % 13 == 0)
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13));
                        }
                        else
                        {
                            pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13) + 1);
                        }
                        int j = 0;

                        for (int i = (AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13 < 0) ? AllFlags.Count - 1 : AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13; i >= 0; i--)
                        {
                            if (j >= 13)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            FlagHistory flag = AllFlags[i] as FlagHistory;

                            flagStatusGridView.Rows.Add();

                            if (flag.GetType().GetField("sploit_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = flag.sploit_name;
                                flagStatusGridView[0, j] = textBoxCell1;
                            }
                            if (flag.GetType().GetField("team_name") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = flag.team_name;
                                flagStatusGridView[1, j] = textBoxCell2;
                            }
                            if (flag.GetType().GetField("sended_flag") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = flag.sended_flag;
                                flagStatusGridView[2, j] = textBoxCell3;
                            }
                            if (flag.GetType().GetField("time") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = flag.time;
                                flagStatusGridView[3, j] = textBoxCell4;
                            }
                            if (flag.GetType().GetField("status") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell5 = new DataGridViewTextBoxCell();
                                textBoxCell5.Value = flag.status;
                                flagStatusGridView[4, j] = textBoxCell5;
                            }
                            if (flag.GetType().GetField("cheskSystemRespons") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell6 = new DataGridViewTextBoxCell();
                                textBoxCell6.Value = flag.cheskSystemRespons;
                                flagStatusGridView[5, j] = textBoxCell6;
                            }

                            j++;
                        }
                    }
                    nextPageFlagsTableButton.Enabled = true;
                    prevPageFlagsTableButton.Enabled = true;
                    nextPageFlagsTableButton.Select();
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while swithing flags pages\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Enabled = true;
                nextPageFlagsTableButton.Select();
            }
        }
        private void curPageFlagsTableTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void pageForFlagsPanelLabel_Click(object sender, EventArgs e)
        {

        }
        private void pagesOfMaxForFlagsPanelLabel_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Teams List Panel
        private void teamsPlaceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void teamsListLabel_Click(object sender, EventArgs e)
        {

        }
        private void AutoTeamsParsFromScoreBoardCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void teamsListPanel_Paint(object sender, PaintEventArgs e)
        {

        }


        private void prevPageTeamsTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                string buf = curPageTeamsTableTextBox.Text;
                curPageTeamsTableTextBox.Text = Convert.ToInt32(curPageTeamsTableTextBox.Text) > 1 ? Convert.ToString(Convert.ToInt32(curPageTeamsTableTextBox.Text) - 1) : Convert.ToString(curPageTeamsTableTextBox.Text);
                if (buf != curPageTeamsTableTextBox.Text)
                {
                    DataBaseWorkAplication DBWorkFormTeam = new DataBaseWorkAplication();
                    DBWorkFormTeam.StartConnection("Data Source=FarmInfo.db");
                    List<object>? ctfteams = DBWorkFormTeam.ReadClassFromDB_AllClass(new CTFTeam());
                    nextPageTeamsTableButton.Enabled = false;
                    prevPageTeamsTableButton.Enabled = false;
                    if (ctfteams != null)
                    {
                        if (ctfteams.Count % 11 == 0)
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfteams.Count - (ctfteams.Count % 11)) / 11);
                        }
                        else
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfteams.Count - (ctfteams.Count % 11)) / 11) + 1);
                        }
                        teamsPlaceDataGridView.Rows.Clear();

                        int i = 0;
                        for (int j = (Convert.ToInt32(curPageTeamsTableTextBox.Text) - 1) * 11; j < ctfteams.Count; j++)
                        {
                            if (i >= 11)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            CTFTeam team = ctfteams[j] as CTFTeam;

                            teamsPlaceDataGridView.Rows.Add();
                            if (team.GetType().GetField("teamPlace") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = team.teamPlace;
                                teamsPlaceDataGridView[0, i] = textBoxCell1;
                            }

                            if (team.GetType().GetField("teamName") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = team.teamName;
                                teamsPlaceDataGridView[1, i] = textBoxCell2;
                            }

                            if (team.GetType().GetField("teamIP") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = team.teamIP;
                                teamsPlaceDataGridView[2, i] = textBoxCell3;
                            }

                            if (team.GetType().GetField("teamScore") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = team.teamScore;
                                teamsPlaceDataGridView[3, i] = textBoxCell4;
                            }

                            i++;
                        }
                        teamsPlaceDataGridView.Sort(teamsPlaceDataGridView.Columns["team_place"], System.ComponentModel.ListSortDirection.Ascending);
                    }
                    nextPageTeamsTableButton.Enabled = true;
                    prevPageTeamsTableButton.Enabled = true;
                    prevPageTeamsTableButton.Select();
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while swithing teams pages\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageTeamsTableButton.Enabled = true;
                prevPageTeamsTableButton.Enabled = true;
                prevPageTeamsTableButton.Select();
            }
        }
        private void nextPageTeamsTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                string buf = curPageTeamsTableTextBox.Text;
                curPageTeamsTableTextBox.Text = Convert.ToInt32(curPageTeamsTableTextBox.Text) < Convert.ToInt32(pagesOfMaxForTeamsPanelLabel.Text.Split(' ')[1]) ? Convert.ToString(Convert.ToInt32(curPageTeamsTableTextBox.Text) + 1) : Convert.ToString(curPageTeamsTableTextBox.Text);
                if (buf != curPageTeamsTableTextBox.Text)
                {
                    DataBaseWorkAplication DBWorkFormTeam = new DataBaseWorkAplication();
                    DBWorkFormTeam.StartConnection("Data Source=FarmInfo.db");
                    List<object>? ctfteams = DBWorkFormTeam.ReadClassFromDB_AllClass(new CTFTeam());
                    nextPageTeamsTableButton.Enabled = false;
                    prevPageTeamsTableButton.Enabled = false;
                    if (ctfteams != null)
                    {
                        if (ctfteams.Count % 11 == 0)
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32(ctfteams.Count - (ctfteams.Count % 11)) / 11);
                        }
                        else
                        {
                            pagesOfMaxForTeamsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((ctfteams.Count - (ctfteams.Count % 11)) / 11) + 1);
                        }
                        teamsPlaceDataGridView.Rows.Clear();

                        int i = 0;
                        for (int j = (Convert.ToInt32(curPageTeamsTableTextBox.Text) - 1) * 11; j < ctfteams.Count; j++)
                        {
                            if (i >= 11)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            CTFTeam team = ctfteams[j] as CTFTeam;

                            teamsPlaceDataGridView.Rows.Add();
                            if (team.GetType().GetField("teamPlace") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                                textBoxCell1.Value = team.teamPlace;
                                teamsPlaceDataGridView[0, i] = textBoxCell1;
                            }

                            if (team.GetType().GetField("teamName") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                                textBoxCell2.Value = team.teamName;
                                teamsPlaceDataGridView[1, i] = textBoxCell2;
                            }

                            if (team.GetType().GetField("teamIP") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                                textBoxCell3.Value = team.teamIP;
                                teamsPlaceDataGridView[2, i] = textBoxCell3;
                            }

                            if (team.GetType().GetField("teamScore") != null)
                            {
                                DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                                textBoxCell4.Value = team.teamScore;
                                teamsPlaceDataGridView[3, i] = textBoxCell4;
                            }

                            i++;
                        }
                        teamsPlaceDataGridView.Sort(teamsPlaceDataGridView.Columns["team_place"], System.ComponentModel.ListSortDirection.Ascending);
                    }
                    nextPageTeamsTableButton.Enabled = true;
                    prevPageTeamsTableButton.Enabled = true;
                    nextPageTeamsTableButton.Select();
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while swithing teams pages\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageTeamsTableButton.Enabled = true;
                prevPageTeamsTableButton.Enabled = true;
                nextPageTeamsTableButton.Select();
            }
        }
        private void curPageTeamsTableTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void pageForTeamsPanelLabel_Click(object sender, EventArgs e)
        {

        }
        private void pagesOfMaxForTeamsPanelLabel_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region Manual Submit Panel
        private void manualFlagSubmitPanelLabel_Click(object sender, EventArgs e)
        {

        }

        private void manualSubmitTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void manualSubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(manualSubmitTextBox.Text))
                {
                    if (Regex.IsMatch(manualSubmitTextBox.Text, fs.flagFormat))
                    {
#if DebagTestFunction                                                                                   ////////////////////////////////////////////////////////////////
                        var thrInfo = new ThreadInfoClass("Manual Test", "some_ip", "Manual Test");
                        thrInfo.flags = new string[] { manualSubmitTextBox.Text };
                        flagsQueue.Enqueue(new KeyValuePair<ThreadInfoClass, DateTime>(thrInfo, DateTime.Now));
#else
                        PythonFlagSendRequest(new string[1] { manualSubmitTextBox.Text }, "Manual Test", "Manual Test");
#endif
                    }
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while manual flag sending\n{exp}");
                }
            }
            finally
            {
                manualSubmitTextBox.Text = "";
                Thread.Sleep(ManualFlagSendCoolDown);
            }
        }

        private void manualSubmitPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
        #region Exploit Test Panel
        private void exploitTestPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void exploitTestPanelLabel_Click(object sender, EventArgs e)
        {

        }

        private void exploitChooseTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void exploitChooseTextBox_Enter(object? sender, EventArgs e)
        {
            runTestButton.Select();
            exploitChooseTextBox.SelectionLength = 0;
        }
        private void exploitChooseTextBox_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Python files(*.py)|*.py";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Choose exploit for test";
            if (!(openFileDialog.ShowDialog() == DialogResult.Cancel))
            {
                exploitChooseTextBox.Text = Path.GetFileName(openFileDialog.FileName);
                exploitPathForTest = openFileDialog.FileName;
            }
        }

        private void teamChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void isTestGoodLabel_Click(object sender, EventArgs e)
        {

        }

        private void runTestButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (teamChooseComboBox.Text != "-Select Team for test-" && exploitChooseTextBox.Text != "-Choose Exploit for test-")
                {
                    DataBaseWorkAplication dbForTest = new DataBaseWorkAplication();
                    dbForTest.StartConnection("Data Source=FarmInfo.db");
                    if (teamChooseComboBox.Text != "All teams")
                    {
                        var team = dbForTest.ReadClassFromDB_OneClass_byParams(new CTFTeam(), new List<string>() { $"teamName='{teamChooseComboBox.Text}'" });
                        if (team != null)
                        {
                            CTFTeam ctfTeam = team as CTFTeam;
                            runScript(new ThreadInfoClass(exploitPathForTest, ctfTeam.teamIP, ctfTeam.teamName));
                        }
                    }
                    else
                    {
                        var teams = dbForTest.ReadClassFromDB_AllClass(new CTFTeam());
                        if (teams != null)
                        {
                            foreach (var team in teams)
                            {
                                CTFTeam ctfTeam = team as CTFTeam;
                                Thread thread = new Thread(runScript);
                                thread.Start(new ThreadInfoClass(exploitPathForTest, ctfTeam.teamIP, ctfTeam.teamName));
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while testing sploit\n{exp}");
                }
            }
            finally
            {
                exploitChooseTextBox.Text = "-Choose Exploit for test-";
                teamChooseComboBox.SelectedIndex = 0;
                Thread.Sleep(SploitTestCoolDown);
            }
        }
        #endregion
        #region Filter Panel
        private void flagShowFilterPanelLabel_Click(object sender, EventArgs e)
        {

        }

        private void exploitFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void teamFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void flagFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void statusFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void cheskSystemResponsFilterTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        #region TextBoxs EventHandlers
        private void exploitFilterTextBox_Enter(object? sender, EventArgs e)
        {
            if (exploitFilterTextBox.Text == ">Exploit Name")
            {
                exploitFilterTextBox.Clear();
                exploitFilterTextBox.ForeColor = Color.Black;
            }
        }
        private void exploitFilterTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(exploitFilterTextBox.Text))
            {
                exploitFilterTextBox.Text = ">Exploit Name";
                exploitFilterTextBox.ForeColor = Color.Gray;
            }
        }

        private void teamFilterTextBox_Enter(object? sender, EventArgs e)
        {
            if (teamFilterTextBox.Text == ">Team Name")
            {
                teamFilterTextBox.Clear();
                teamFilterTextBox.ForeColor = Color.Black;
            }
        }
        private void teamFilterTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teamFilterTextBox.Text))
            {
                teamFilterTextBox.Text = ">Team Name";
                teamFilterTextBox.ForeColor = Color.Gray;
            }
        }

        private void flagFilterTextBox_Enter(object? sender, EventArgs e)
        {
            if (flagFilterTextBox.Text == ">Flag")
            {
                flagFilterTextBox.Clear();
                flagFilterTextBox.ForeColor = Color.Black;
            }
        }
        private void flagFilterTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(flagFilterTextBox.Text))
            {
                flagFilterTextBox.Text = ">Flag";
                flagFilterTextBox.ForeColor = Color.Gray;
            }
        }

        private void statusFilterTextBox_Enter(object? sender, EventArgs e)
        {
            if (statusFilterTextBox.Text == ">Status")
            {
                statusFilterTextBox.Clear();
                statusFilterTextBox.ForeColor = Color.Black;
            }
        }
        private void statusFilterTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(statusFilterTextBox.Text))
            {
                statusFilterTextBox.Text = ">Status";
                statusFilterTextBox.ForeColor = Color.Gray;
            }
        }

        private void cheskSystemResponsFilterTextBox_Enter(object? sender, EventArgs e)
        {
            if (cheskSystemResponsFilterTextBox.Text == ">Chesk System Respons")
            {
                cheskSystemResponsFilterTextBox.Clear();
                cheskSystemResponsFilterTextBox.ForeColor = Color.Black;
            }
        }
        private void cheskSystemResponsFilterTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cheskSystemResponsFilterTextBox.Text))
            {
                cheskSystemResponsFilterTextBox.Text = ">Chesk System Respons";
                cheskSystemResponsFilterTextBox.ForeColor = Color.Gray;
            }
        }
        #endregion

        private void applyFilterButton_Click(object sender, EventArgs e)
        {
            filtersForFlagShow.Clear();
            try
            {
                if (exploitFilterTextBox.Text != ">Exploit Name" && !string.IsNullOrWhiteSpace(exploitFilterTextBox.Text))
                {
                    filtersForFlagShow.Add($"sploit_name='{exploitFilterTextBox.Text}'");
                }
                if (teamFilterTextBox.Text != ">Team Name" && !string.IsNullOrWhiteSpace(teamFilterTextBox.Text))
                {
                    filtersForFlagShow.Add($"team_name='{teamFilterTextBox.Text}'");
                }
                if (flagFilterTextBox.Text != ">Flag" && !string.IsNullOrWhiteSpace(flagFilterTextBox.Text))
                {
                    filtersForFlagShow.Add($"sended_flag='{flagFilterTextBox.Text}'");
                }
                if (statusFilterTextBox.Text != ">Status" && !string.IsNullOrWhiteSpace(statusFilterTextBox.Text))
                {
                    filtersForFlagShow.Add($"status='{statusFilterTextBox.Text}'");
                }
                if (cheskSystemResponsFilterTextBox.Text != ">Chesk System Respons" && !string.IsNullOrWhiteSpace(cheskSystemResponsFilterTextBox.Text))
                {
                    filtersForFlagShow.Add($"cheskSystemRespons='{cheskSystemResponsFilterTextBox.Text}'");
                }

                if (ClearFiltersInputCheckBox.Checked)
                {
                    exploitFilterTextBox.Text = ">Exploit Name";
                    exploitFilterTextBox.ForeColor = Color.Gray;

                    teamFilterTextBox.Text = ">Team Name";
                    teamFilterTextBox.ForeColor = Color.Gray;

                    flagFilterTextBox.Text = ">Flag";
                    flagFilterTextBox.ForeColor = Color.Gray;

                    statusFilterTextBox.Text = ">Status";
                    statusFilterTextBox.ForeColor = Color.Gray;

                    cheskSystemResponsFilterTextBox.Text = ">Chesk System Respons";
                    cheskSystemResponsFilterTextBox.ForeColor = Color.Gray;
                }

                DataBaseWorkAplication DBWorkFormFlagFilter = new DataBaseWorkAplication();
                DBWorkFormFlagFilter.StartConnection("Data Source=FarmInfo.db");
                List<object>? AllFlags = new List<object>();
                if (filtersForFlagShow.Count > 0)
                {
                    AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass_byParams(new FlagHistory(), filtersForFlagShow);
                }
                else
                {
                    AllFlags = DBWorkFormFlagFilter.ReadClassFromDB_AllClass(new FlagHistory()); ;
                }


                nextPageFlagsTableButton.Enabled = false;
                prevPageFlagsTableButton.Enabled = false;

                flagStatusGridView.Rows.Clear();
                if (AllFlags != null)
                {
                    if (AllFlags.Count % 13 == 0)
                    {
                        pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13));
                    }
                    else
                    {
                        pagesOfMaxForFlagsPanelLabel.Text = "of " + Convert.ToString(Convert.ToInt32((AllFlags.Count - (AllFlags.Count % 13)) / 13) + 1);
                    }
                    int j = 0;

                    for (int i = (AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13 < 0) ? AllFlags.Count - 1 : AllFlags.Count - 1 - (Convert.ToInt32(curPageFlagsTableTextBox.Text) - 1) * 13; i >= 0; i--)
                    {
                        if (j >= 13)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        {
                            break;
                        }
                        FlagHistory flag = AllFlags[i] as FlagHistory;

                        flagStatusGridView.Rows.Add();

                        if (flag.GetType().GetField("sploit_name") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                            textBoxCell1.Value = flag.sploit_name;
                            flagStatusGridView[0, j] = textBoxCell1;
                        }
                        if (flag.GetType().GetField("team_name") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                            textBoxCell2.Value = flag.team_name;
                            flagStatusGridView[1, j] = textBoxCell2;
                        }
                        if (flag.GetType().GetField("sended_flag") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                            textBoxCell3.Value = flag.sended_flag;
                            flagStatusGridView[2, j] = textBoxCell3;
                        }
                        if (flag.GetType().GetField("time") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                            textBoxCell4.Value = flag.time;
                            flagStatusGridView[3, j] = textBoxCell4;
                        }
                        if (flag.GetType().GetField("status") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell5 = new DataGridViewTextBoxCell();
                            textBoxCell5.Value = flag.status;
                            flagStatusGridView[4, j] = textBoxCell5;
                        }
                        if (flag.GetType().GetField("cheskSystemRespons") != null)
                        {
                            DataGridViewTextBoxCell textBoxCell6 = new DataGridViewTextBoxCell();
                            textBoxCell6.Value = flag.cheskSystemRespons;
                            flagStatusGridView[5, j] = textBoxCell6;
                        }

                        j++;
                    }
                }
                nextPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Enabled = true;
            }
            catch (Exception exp)
            {
                if (!warningsToolStripMenuItem.Checked)
                {
                    MessageBox.Show($"Warning!\nSome problem while applying filters\n{exp}", "WARNING");
                }
            }
            finally
            {
                nextPageFlagsTableButton.Enabled = true;
                prevPageFlagsTableButton.Enabled = true;
            }
        }

        private void ClearFiltersInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void flagShowFilterPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion


        #region Button EventHandlers
        private void manualSubmitButton_Enter(object? sender, EventArgs e)
        {
            manualSubmitButton.BackColor = Color.Purple;
        }
        private void manualSubmitButton_Leave(object? sender, EventArgs e)
        {
            manualSubmitButton.BackColor = Color.Black;
        }

        private void runTestButton_Enter(object? sender, EventArgs e)
        {
            runTestButton.BackColor = Color.Purple;
        }
        private void runTestButton_Leave(object? sender, EventArgs e)
        {
            runTestButton.BackColor = Color.Black;
        }

        private void applyFilterButton_Enter(object? sender, EventArgs e)
        {
            applyFilterButton.BackColor = Color.Purple;
        }
        private void applyFilterButton_Leave(object? sender, EventArgs e)
        {
            applyFilterButton.BackColor = Color.Black;
        }

        private void nextPageFlagsTableButton_Enter(object? sender, EventArgs e)
        {
            nextPageFlagsTableButton.BackColor = Color.Purple;
        }
        private void nextPageFlagsTableButton_Leave(object? sender, EventArgs e)
        {
            nextPageFlagsTableButton.BackColor = Color.Black;
        }

        private void prevPageFlagsTableButton_Enter(object? sender, EventArgs e)
        {
            prevPageFlagsTableButton.BackColor = Color.Purple;
        }
        private void prevPageFlagsTableButton_Leave(object? sender, EventArgs e)
        {
            prevPageFlagsTableButton.BackColor = Color.Black;
        }

        private void nextPageTeamsTableButton_Enter(object? sender, EventArgs e)
        {
            nextPageTeamsTableButton.BackColor = Color.Purple;
        }
        private void nextPageTeamsTableButton_Leave(object? sender, EventArgs e)
        {
            nextPageTeamsTableButton.BackColor = Color.Black;
        }

        private void prevPageTeamsTableButton_Enter(object? sender, EventArgs e)
        {
            prevPageTeamsTableButton.BackColor = Color.Purple;
        }
        private void prevPageTeamsTableButton_Leave(object? sender, EventArgs e)
        {
            prevPageTeamsTableButton.BackColor = Color.Black;
        }
        #endregion
    }
}