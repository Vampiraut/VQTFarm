using System.Diagnostics;
using System.Text.Json;

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

        private string exploitPathForTest;

        private List<string> filtersForFlagShow;
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


                setFlagStatusGridView();
                setTeamsPlaceDataGridView();

                if (AutoTeamsParsFromScoreBoardCheckBox.Checked)
                {
                    PythonGetRequest(false);
                }

                List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
                if (ctfTeams != null)
                {
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
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error!\nSome problem while loadin farm\n{exp}", "ERROR");
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

                flagStatusGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
                MessageBox.Show($"Warning!\nSome problem with set settings for Flag Status DataGridView\n{exp}", "WARNING");
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

                teamsPlaceDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                teamsPlaceDataGridView.Width = teamsPlaceDataGridView.Columns[0].Width +
                    teamsPlaceDataGridView.Columns[1].Width +
                    teamsPlaceDataGridView.Columns[2].Width +
                    teamsPlaceDataGridView.Columns[3].Width + 20;

                teamsPlaceDataGridView.ClearSelection();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem with set settings for Teams Place DataGridView\n{exp}", "WARNING");
            }
        }
        #endregion


        #region Threadusable functions
        private void PythonFlagSendRequest(string[] flags, string teamName, string exploitName)
        {
            ProcessStartInfo start = new ProcessStartInfo()
            {
                FileName = "python.exe",
                Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", fs.pythonFlagSendScriptPath, fs.flagSubmitterURL, JsonSerializer.Serialize(flags), fs.teamToken, teamName, exploitName),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
            };
            Process.Start(start);
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
                Process.Start(start);
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem with parsing script\n{exp}", "WARNING");
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
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        thrInfo.flags = reader.ReadToEnd().Split(',');
                    }
                }
                flagsQueue.Enqueue(new KeyValuePair<ThreadInfoClass, DateTime>(thrInfo, DateTime.Now));
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem with script running\n{exp}", "WARNING");
            }
        }
        #endregion
        #region Threading Events
        private void StopThreadings()
        {
            try
            {
                isFailSafeTHRMustRun = false;
                Thread.Sleep(FailSafeTHRCoolDown + 1000);
                isTeamsUpdateTHRMustRun = false;
                isFlagsUpdateTHRMustRun = false;
                isSploitDirectoryCheckTHRMustRun = false;
                isAutoSploitRunTHRMustRun = false;
                isFlagsSendTHRMustRun = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error!\nSome problem in stopping threadings.\nPlease, close threadings by using Task Manager and restart Farm\n{exp}", "ERROR");
            }
        }
        private void failSafe()
        {
            try
            {
                while (isFailSafeTHRMustRun)
                {
                    Thread.Sleep(FailSafeTHRCoolDown);
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
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error!\nSome problem in restart multiprocessing.\nPlease, restart farm for correct work!\n{exp}", "ERROR");
            }
        }
        private void flagsSend()
        {
            try
            {
                while (isFlagsSendTHRMustRun)
                {
                    var isQueueContainsFlags = flagsQueue.TryDequeue(out KeyValuePair<ThreadInfoClass, DateTime> thrInfo);
                    if (isQueueContainsFlags)
                    {
                        if (thrInfo.Value.AddMilliseconds(fs.roundTime * fs.flagLifeTime) >= DateTime.Now)
                        {
                            PythonFlagSendRequest(thrInfo.Key.flags, thrInfo.Key.team_name, Path.GetFileName(thrInfo.Key.scriptPath));
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
                MessageBox.Show($"Warning!\nSome problem while flag sending\n{exp}", "WARNING");
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
                    if (AutoTeamsParsFromScoreBoardCheckBox.Checked)
                    {
                        PythonGetRequest(true);
                    }
                    List<object>? ctfteams = DBWorkFormTeam.ReadClassFromDB_AllClass(new CTFTeam());
                    if (ctfteams != null)
                    {
                        if (ctfteams.Count <= 0)
                        {
                            Thread.Sleep(fs.roundTime);
                            continue;
                        }
                        teamsPlaceDataGridView.Rows.Clear();

                        int i = 0;
                        foreach (var ctfteam in ctfteams)
                        {
                            CTFTeam team = ctfteam as CTFTeam;
                            
                            teamsPlaceDataGridView.Rows.Add();
                            if(team.GetType().GetField("teamPlace") != null)
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
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while updating teams info\n{exp}", "WARNING");
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
                MessageBox.Show($"Warning!\nSome problem while auto run sploits\n{exp}", "WARNING");
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

                    if (AllFlags != null)
                    {
                        flagStatusGridView.Rows.Clear();

                        int j = 0;

                        for (int i = AllFlags.Count - 1; i >= 0; i--)
                        {
                            if (j >= 13)///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            {
                                break;
                            }
                            FlagHistory flag = AllFlags[i] as FlagHistory;

                            flagStatusGridView.Rows.Add();

                            DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                            textBoxCell1.Value = flag.sploit_name;
                            flagStatusGridView[0, j] = textBoxCell1;

                            DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                            textBoxCell2.Value = flag.team_name;
                            flagStatusGridView[1, j] = textBoxCell2;

                            DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                            textBoxCell3.Value = flag.sended_flag;
                            flagStatusGridView[2, j] = textBoxCell3;

                            DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                            textBoxCell4.Value = flag.time;
                            flagStatusGridView[3, j] = textBoxCell4;

                            DataGridViewTextBoxCell textBoxCell5 = new DataGridViewTextBoxCell();
                            textBoxCell5.Value = flag.status;
                            flagStatusGridView[4, j] = textBoxCell5;

                            DataGridViewTextBoxCell textBoxCell6 = new DataGridViewTextBoxCell();
                            textBoxCell6.Value = flag.cheskSystemRespons;
                            flagStatusGridView[5, j] = textBoxCell6;

                            j++;
                        }
                    }
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while updating flag history\n{exp}", "WARNING");
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
                MessageBox.Show($"Warning!\nSome problem while reading sploits from directory{exp}", "WARNING");
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
            List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
            if (ctfTeams != null)
            {
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

        #endregion

        #region Help Menu
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                    PythonFlagSendRequest(new string[1] { manualSubmitTextBox.Text }, "Manual Test", "Manual Test");
                    manualSubmitTextBox.Text = "";
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while manual flag sending\n{exp}");
            }
            finally
            {
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
                MessageBox.Show($"Warning!\nSome problem while testing sploit\n{exp}");
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
        }

        private void ClearFiltersInputCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void flagShowFilterPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
}