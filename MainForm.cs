#define POST
#define DEBAG
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using System.Text.RegularExpressions;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace VQTFarm
{
    public partial class MainForm : Form
    {
        private FarmSettings fs;    //настройки фермы, заданные в начальном окне (формат флага, время раунда и тп)
        private DataBaseWorkAplication DBWorkForm;  //подключённая бдшка


        private Thread TeamsUpdateTHR;  //поток обновления данных о командах
        private bool isTeamsUpdateTHRMustRun; //ОПРОС RoundTime

        private Thread FlagsUpdateTHR;  //поток обновления данных о флагах
        private bool isFlagsUpdateTHRMustRun; //ОПРОС RoundTime

        private Thread SploitDirectoryCheckTHR; //поток проверки папки Sploits на наличие сплоитов, добавление их в общий процесс
        private bool isSploitDirectoryCheckTHRMustRun; //ОПРОС GLOBALTimePauseForThreadings

        private Thread AutoSploitRunTHR;    //поток автоматического запуска сплоитов в делай раунд тайма
        private bool isAutoSploitRunTHRMustRun; //ОПРОС RoundTime

        private Thread FailSafeTHR; //поток автозапуска потоков при их непредвиденной остановке
        private bool isFailSafeTHRMustRun; //ОПРОС GLOBALTimePauseForThreadings

        private List<KeyValuePair<string, string>> teamsAttaskTHRs; //Словарь содержащий IP команды для атаки и список потоков, с подключёнными к ним функциями, являющиеся функциями запуска скриптов

        private List<string> SploitsList;  //список сплоитов(полный путь)

        private int systemSafeCheck; //Защита системы (при привышении лимита (указан ниже) закрывает программу
        private const int ErrorsCountToCloseForm = 15;

        private const int GLOBALTimePauseForThreadings = 20000;

        private Queue<KeyValuePair<ThreadInfoClass, DateTime>> flagsQueue;
        private Thread FlagsSendTHR;
        private bool isFlagsSendTHRMustRun;

        public MainForm(FarmSettings fs, DataBaseWorkAplication DBWorkForm)
        {
            this.fs = fs;
            this.DBWorkForm = DBWorkForm;
            isTeamsUpdateTHRMustRun = true;
            isFlagsUpdateTHRMustRun = true;
            isSploitDirectoryCheckTHRMustRun = true;
            isAutoSploitRunTHRMustRun = true;
            isFailSafeTHRMustRun = true;

            teamsAttaskTHRs = new List<KeyValuePair<string, string>>();

            SploitsList = new List<string>();

            systemSafeCheck = 0;

            flagsQueue = new Queue<KeyValuePair<ThreadInfoClass, DateTime>>();
            isFlagsSendTHRMustRun = true;
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.DoubleBuffered = true;

                isTestGoodLabel.Visible = false;

                exploitChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                teamChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                exploitChooseComboBox.Items.Add("-Choose Exploit-");
                exploitChooseComboBox.SelectedItem = "-Choose Exploit-";

                teamChooseComboBox.Items.Add("-Select Team for test-");
                teamChooseComboBox.SelectedItem = "-Select Team for test-";

                setFlagStatusGridView();
                setTeamsPlaceDataGridView();

                this.FormClosed += new FormClosedEventHandler(MainForm_Closed);

                List<object>? ctfTeams = DBWorkForm.ReadClassFromDB_AllClass(new CTFTeam());
                if (ctfTeams != null)
                {
                    foreach (var teams in ctfTeams) //создаёт постоянную с IP команд и списком потоков
                    {
                        CTFTeam team = teams as CTFTeam;
                        if (team.teamIP != fs.teamOwnerIP)
                        {
                            teamsAttaskTHRs.Add(new KeyValuePair<string, string>(team.teamIP, team.teamName));
                        }
                    }
                }

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
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error!\nSome problem while loadin farm\n{exp}", "ERROR");
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
                systemSafeCheck += 1;
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
                systemSafeCheck += 1;
            }
        }
        #endregion

#if PUT
        private string PutRequest(string flag)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                HttpContent content = new StringContent(flag);
                content.Headers.Add("X-Team-Token", fs.teamToken);

                return httpClient.PutAsync(fs.flaSubmitterURL, content).Result.Content.ReadAsStringAsync().Result.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while do PUT request\n{exp}", "WARNING");
                systemSafeCheck += 1;
                return "ERROR";
            }
            finally
            {
                httpClient.Dispose();
            }
        }
#endif
#if POST
        private string PostRequest(string flag)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(new
                    {
                        flag = flag,
                        token = fs.teamToken
                    }),
                    Encoding.UTF8,
                    "application/json");
                return httpClient.PostAsync(fs.flaSubmitterURL, jsonContent).Result.Content.ReadAsStringAsync().Result.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while do POST request\n{exp}", "WARNING");
                systemSafeCheck += 1;
                return "ERROR";
            }
            finally
            {
                httpClient.Dispose();
            }
        }
#endif
        private string GetRequest()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                return httpClient.GetAsync(fs.scoreboardURL).Result.Content.ReadAsStringAsync().Result.ToString();
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while do GET request\n{exp}", "WARNING");
                systemSafeCheck += 1;
                return "ERROR";
            }
            finally
            {
                httpClient.Dispose();
            }
        }

        private void updateAllTeams(DataBaseWorkAplication DBWorkFormTeam)
        {
            try
            {
                List<CTFTeam> ctfTeams = new List<CTFTeam>();
                string html = GetRequest();

                MatchCollection teamNames = Regex.Matches(html, @"<div class=""teamClass"">(.+)</div>", RegexOptions.Singleline);
                MatchCollection teamIPs = Regex.Matches(html, @"<div class=""teamClass"">(.+)</div>", RegexOptions.Singleline);
                MatchCollection teamScores = Regex.Matches(html, @"<div class=""teamClass"">(.+)</div>", RegexOptions.Singleline);

                for (int i = 0; i < teamNames.Count; i++)
                {
                    ctfTeams.Add(new CTFTeam(Convert.ToString(i), teamNames[i].Groups[1].Value, teamIPs[i].Groups[1].Value, teamScores[i].Groups[1].Value));
                }
                foreach (var team in ctfTeams)
                {
                    DBWorkFormTeam.UpdateClassInDB_byParams(team, new List<string>() { $"teamName={team.teamName}" });
                }
            }
            catch (Exception exs)
            {
                MessageBox.Show("Warning!\nSome problem while try to parse html code", "WARNING");
                systemSafeCheck += 1;
            }
        } //дописать
        private void runScript(object? obj)
        {
            try
            {
                ThreadInfoClass thrInfo = obj as ThreadInfoClass;

                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();

                engine.ExecuteFile(thrInfo.scriptPath, scope);

                dynamic script = scope.GetVariable("script");
                dynamic result = script(thrInfo.ip);
                thrInfo.flag = result.ToString();
#if DEBAG
                flagsQueue.Enqueue(new KeyValuePair<ThreadInfoClass, DateTime>(thrInfo, DateTime.Now));
#endif
#if !DEBAG
                if (fs.flagFormat.Matches(thrInfo.flag).Count > 0)
                {
                    flagsQueue.Enqueue(new KeyValuePair<ThreadInfoClass, DateTime>(thrInfo, DateTime.Now));
                }
#endif
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem with script running\n{exp}", "WARNING");
                systemSafeCheck += 1;
            }
        }

        #region Threading Events
        private void StopThreadings()
        {
            try
            {
                isFailSafeTHRMustRun = false;
                Thread.Sleep(GLOBALTimePauseForThreadings + 1000);
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
#if POST
                            string answer = PostRequest(thrInfo.Key.flag);
#endif
#if PUT
                            string answer = PutRequest(thrInfo.Key.flag);
#endif
                            DataBaseWorkAplication dbWorkFormFlagsAdd = new DataBaseWorkAplication();
                            dbWorkFormFlagsAdd.StartConnection("Data Source=FarmInfo.db");
                            dbWorkFormFlagsAdd.AddClassToDB(new FlagHistory(Path.GetFileName(thrInfo.Key.scriptPath), thrInfo.Key.team_name, thrInfo.Key.flag, DateTime.Now.ToString(), "123", "123"));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while flag sending\n{exp}", "WARNING");
                systemSafeCheck += 1;
            }
        }
        private void failSafe()
        {
            try
            {
                while (isFailSafeTHRMustRun)
                {
                    Thread.Sleep(GLOBALTimePauseForThreadings);
                    if (TeamsUpdateTHR.ThreadState != ThreadState.Running && TeamsUpdateTHR.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        isTeamsUpdateTHRMustRun = true;
                        TeamsUpdateTHR = new Thread(teamUpdate);
                        TeamsUpdateTHR.Start();
                    }
                    if (FlagsUpdateTHR.ThreadState != ThreadState.Running && FlagsUpdateTHR.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        isFlagsUpdateTHRMustRun = true;
                        FlagsUpdateTHR = new Thread(flagsUpdate);
                        FlagsUpdateTHR.Start();
                    }
                    if (SploitDirectoryCheckTHR.ThreadState != ThreadState.Running && SploitDirectoryCheckTHR.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        isSploitDirectoryCheckTHRMustRun = true;
                        SploitDirectoryCheckTHR = new Thread(sploitDirectoryCheck);
                        SploitDirectoryCheckTHR.Start();
                    }
                    if (AutoSploitRunTHR.ThreadState != ThreadState.Running && AutoSploitRunTHR.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        isAutoSploitRunTHRMustRun = true;
                        AutoSploitRunTHR = new Thread(autoSploitRun);
                        AutoSploitRunTHR.Start();
                    }
                    if (FlagsSendTHR.ThreadState != ThreadState.Running && FlagsSendTHR.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        isFlagsSendTHRMustRun = true;
                        FlagsSendTHR = new Thread(flagsSend);
                        FlagsSendTHR.Start();
                    }

                    if (systemSafeCheck >= ErrorsCountToCloseForm)
                    {
                        MessageBox.Show("Error!\nToo many errors have been received!\nThe Farm try to restart automatically.\nIf the errors continues, please restart manually.", "ERROR");
                        this.Close();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error!\nSome problem in restart multiprocessing.\nPlease, restart farm for correct work!\n{exp}", "ERROR");
                systemSafeCheck += 1;
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
                    updateAllTeams(DBWorkFormTeam);
                    List<object>? ctfteams = DBWorkFormTeam.ReadClassFromDB_AllClass(new CTFTeam());
                    if (ctfteams != null)
                    {
                        teamsPlaceDataGridView.Rows.Clear();

                        int i = 0;
                        foreach (var ctfteam in ctfteams)
                        {
                            CTFTeam team = ctfteam as CTFTeam;

                            teamsPlaceDataGridView.Rows.Add();

                            DataGridViewTextBoxCell textBoxCell1 = new DataGridViewTextBoxCell();
                            textBoxCell1.Value = team.teamPlace;
                            teamsPlaceDataGridView[0, i] = textBoxCell1;

                            DataGridViewTextBoxCell textBoxCell2 = new DataGridViewTextBoxCell();
                            textBoxCell2.Value = team.teamName;
                            teamsPlaceDataGridView[1, i] = textBoxCell2;

                            DataGridViewTextBoxCell textBoxCell3 = new DataGridViewTextBoxCell();
                            textBoxCell3.Value = team.teamIP;
                            teamsPlaceDataGridView[2, i] = textBoxCell3;

                            DataGridViewTextBoxCell textBoxCell4 = new DataGridViewTextBoxCell();
                            textBoxCell4.Value = team.teamScore;
                            teamsPlaceDataGridView[3, i] = textBoxCell4;

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
                systemSafeCheck += 1;
            }
        }
        private void autoSploitRun()
        {
            try
            {
                while (isAutoSploitRunTHRMustRun)
                {
                    foreach (var team in teamsAttaskTHRs)
                    {
                        for (int i = 0; i < SploitsList.Count; i++)
                        {
                            Thread thread = new Thread(runScript);
                            thread.Start(new ThreadInfoClass(SploitsList[i], team.Key, team.Value));
                        }
                    }
                    Thread.Sleep(fs.roundTime);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while auto run sploits\n{exp}", "WARNING");
                systemSafeCheck += 1;
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
                    List<object>? AllFlags = DBWorkFormFlag.ReadClassFromDB_AllClass(new FlagHistory());
                    if (AllFlags != null)
                    {
                        flagTotalAceptedLabel.Text = $"TOTAL FLAGS ACCEPTED: {AllFlags.Count}";

                        flagStatusGridView.Rows.Clear();

                        int j = 0;

                        for (int i = AllFlags.Count - 1; i >= 0; i--)
                        {
                            if (j >= 10)//////////////////////////////////////////////////////////////////
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
                systemSafeCheck += 1;
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
                            if (!SploitsList.Contains(item) || !exploitChooseComboBox.Items.Contains(Path.GetFileName(item)))
                            {
                                exploitChooseComboBox.Items.Clear();
                                exploitChooseComboBox.Items.Add("-Choose Exploit-");
                                exploitChooseComboBox.SelectedItem = "-Choose Exploit-";

                                SploitsList.Clear();
                                foreach (var sploit in sploits)
                                {
                                    SploitsList.Add(sploit);
                                    exploitChooseComboBox.Items.Add(Path.GetFileName(sploit));
                                }
                            }
                        }
                    }
                    else
                    {
                        exploitChooseComboBox.Items.Clear();
                        exploitChooseComboBox.Items.Add("-Choose Exploit-");
                        exploitChooseComboBox.SelectedItem = "-Choose Exploit-";
                        SploitsList.Clear();
                    }
                    Thread.Sleep(GLOBALTimePauseForThreadings);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while reading sploits from directory{exp}", "WARNING");
                systemSafeCheck += 1;
            }
        }
#endregion

        #region Menu Strip
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Flag Status Grid View
        private void flagStatusGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void flagStatusLabel_Click(object sender, EventArgs e)
        {

        }
        private void flagTotalAceptedLabel_Click(object sender, EventArgs e)
        {

        }
        private void flagStatusPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region Teams List Grid View
        private void teamsPlaceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void teamsListLabel_Click(object sender, EventArgs e)
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
#if PUT
                    string answer = PutRequest(manualSubmitTextBox.Text);
#endif
#if POST
                    string answer = PostRequest(manualSubmitTextBox.Text);
#endif
#if DEBUG
                    MessageBox.Show(answer, "Answer");
#endif
                    DataBaseWorkAplication dbWorkFormFlagsAdd = new DataBaseWorkAplication();
                    dbWorkFormFlagsAdd.StartConnection("Data Source=FarmInfo.db");
                    dbWorkFormFlagsAdd.AddClassToDB(new FlagHistory("Manual Test", "Manual Test", manualSubmitTextBox.Text, DateTime.Now.ToString(), "123", answer));/////////////////////
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while manual flag sending\n{exp}");
                systemSafeCheck += 1;
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

        private void exploitChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                if (teamChooseComboBox.Text != "-Select Team for test-" && exploitChooseComboBox.Text != "-Choose Exploit-")
                {
                    DataBaseWorkAplication dbForTest = new DataBaseWorkAplication();
                    dbForTest.StartConnection("Data Source=FarmInfo.db");

                    var team = dbForTest.ReadClassFromDB_OneClass_byParams(new CTFTeam(), new List<string>() { $"teamName={teamChooseComboBox.Text}" });
                    CTFTeam ctfTeam = team as CTFTeam;
                    var sploits = Directory.GetFiles("Sploits");
                    foreach (var sploit in sploits)
                    {
                        if (Path.GetFileName(sploit) == exploitChooseComboBox.Text)
                        {
                            runScript(new ThreadInfoClass(sploit, ctfTeam.teamIP, ctfTeam.teamName));
                            break;
                        }
                    }
                    isTestGoodLabel.Text = "Successfully";
                    isTestGoodLabel.ForeColor = Color.Green;
                    isTestGoodLabel.Visible = true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while testing sploit\n{exp}");
                systemSafeCheck += 1;
                isTestGoodLabel.Text = "Unsuccessful";
                isTestGoodLabel.ForeColor = Color.Red;
                isTestGoodLabel.Visible = true;
            }
            finally
            {
                Thread.Sleep(GLOBALTimePauseForThreadings);
            }
        }
        #endregion

        #region Filter Panel
        private void flagShowFilterPanelLabel_Click(object sender, EventArgs e)
        {

        }

        private void flagShowFilterPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
    }
}