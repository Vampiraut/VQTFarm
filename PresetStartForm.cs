using System.Text.Json;
using System.Text.RegularExpressions;

namespace VQTFarm
{
    public partial class PresetStartForm : Form
    {
        DataBaseWorkAplication DBWorkForm = new DataBaseWorkAplication();
        FarmSettings fs;

        Thread DirectoryCheckForScriptsTHR;
        bool isDirectoryCheckForScriptsTHRMustRun = true;
        public PresetStartForm()
        {
            DBWorkForm.StartConnection("Data Source=FarmInfo.db");
            DBWorkForm.CreateTableByClass(new CTFTeam());
            DBWorkForm.CreateTableByClass(new FlagHistory());


            if (File.Exists("settings.json"))
            {
                using (StreamReader sr = new StreamReader("settings.json"))
                {
                    this.fs = JsonSerializer.Deserialize<FarmSettings>(sr.ReadToEnd());
                }
            }

            InitializeComponent();
        }
        private void PresetStartForm_Load(object sender, EventArgs e)
        {
            deployButton.Select();

            noFlagFormatLabel.Visible = false;
            noTeamTokenLabel.Visible = false;
            noTeamOwnerIPLabel.Visible = false;
            noRoundTimeLabel.Visible = false;
            noFlagLifeTimeLabel.Visible = false;
            noScoreboardURLLabel.Visible = false;
            noFlagsubmitterURLLabel.Visible = false;

            noPythonGetScriptLabel.Visible = false;
            noPythonFlagSendScriptLabel.Visible = false;


            pythonGetScriptChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pythonFlagSendScriptChooseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            pythonGetScriptChooseComboBox.Items.Add("-Select Get Script-");
            pythonGetScriptChooseComboBox.SelectedItem = "-Select Get Script-";

            pythonFlagSendScriptChooseComboBox.Items.Add("-Select FlagSend Script-");
            pythonFlagSendScriptChooseComboBox.SelectedItem = "-Select FlagSend Script-";

            DirectoryCheckForScriptsTHR = new Thread(threadUpdateScripts);
            DirectoryCheckForScriptsTHR.Start();

            #region EventHandlers
            deployButton.MouseEnter += new EventHandler(deployButton_Enter);
            deployButton.MouseLeave += new EventHandler(deployButton_Leave);


            flagFormatTextBox.Enter += new EventHandler(flagFormatTextBox_Enter);
            flagFormatTextBox.Leave += new EventHandler(flagFormatTextBox_Leave);

            teamTokenTextBox.Enter += new EventHandler(teamTokenTextBox_Enter);
            teamTokenTextBox.Leave += new EventHandler(teamTokenTextBox_Leave);

            teamOwnerIPTextBox.Enter += new EventHandler(teamOwnerIPTextBox_Enter);
            teamOwnerIPTextBox.Leave += new EventHandler(teamOwnerIPTextBox_Leave);

            roundTimeTextBox.Enter += new EventHandler(roundTimeTextBox_Enter);
            roundTimeTextBox.Leave += new EventHandler(roundTimeTextBox_Leave);

            flagLifeTimeTextBox.Enter += new EventHandler(flagLifeTimeTextBox_Enter);
            flagLifeTimeTextBox.Leave += new EventHandler(flagLifeTimeTextBox_Leave);

            scoreBoardURLTextBox.Enter += new EventHandler(scoreBoardURLTextBox_Enter);
            scoreBoardURLTextBox.Leave += new EventHandler(scoreBoardURLTextBox_Leave);

            flagSubmitterURLTextBox.Enter += new EventHandler(flagSubmitterURLTextBox_Enter);
            flagSubmitterURLTextBox.Leave += new EventHandler(flagSubmitterURLTextBox_Leave);

            this.FormClosed += new FormClosedEventHandler(PresetStartForm_Closed);
            #endregion

            StartGhostTextSet();
        }

        private void PresetStartForm_Closed(object? sender, FormClosedEventArgs e)
        {
            isDirectoryCheckForScriptsTHRMustRun = false;
            if (fs != null)
            {
                if (MessageBox.Show("Do you want to save the specified settings before exiting?", "Turning Off the Farm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (StreamWriter sr = new StreamWriter("settings.json"))
                    {
                        string jsonStr = JsonSerializer.Serialize(fs);
                        sr.WriteLine(jsonStr);
                    }
                }
                else
                {
                    if (File.Exists("settings.json"))
                    {
                        File.Delete("settings.json");
                    }
                }
            }
        }

        public void StartGhostTextSet()
        {
            if (fs == null)
            {
                flagFormatTextBox.Text = ">Regex flag format";
                flagFormatTextBox.ForeColor = Color.Gray;

                teamTokenTextBox.Text = ">X-Team-Token";
                teamTokenTextBox.ForeColor = Color.Gray;

                teamOwnerIPTextBox.Text = ">Your Team IP";
                teamOwnerIPTextBox.ForeColor = Color.Gray;

                roundTimeTextBox.Text = ">Round Time (in sec)";
                roundTimeTextBox.ForeColor = Color.Gray;

                flagLifeTimeTextBox.Text = ">Flag Lifetime(in rounds)";
                flagLifeTimeTextBox.ForeColor = Color.Gray;

                scoreBoardURLTextBox.Text = ">Scoreboard URL";
                scoreBoardURLTextBox.ForeColor = Color.Gray;

                flagSubmitterURLTextBox.Text = ">Flag Submitter URL";
                flagSubmitterURLTextBox.ForeColor = Color.Gray;
            }
            else
            {
                flagFormatTextBox.Text = fs.flagFormat;
                teamTokenTextBox.Text = fs.teamToken;
                teamOwnerIPTextBox.Text = fs.teamOwnerIP;
                roundTimeTextBox.Text = Convert.ToString(Convert.ToInt32(fs.roundTime / 1000));
                flagLifeTimeTextBox.Text = Convert.ToString(fs.flagLifeTime);
                scoreBoardURLTextBox.Text = fs.scoreBoardURL;
                flagSubmitterURLTextBox.Text = fs.flagSubmitterURL;

                Thread.Sleep(5020);
                pythonGetScriptChooseComboBox.SelectedItem = Path.GetFileName(fs.pythonGetScriptPath);
                pythonFlagSendScriptChooseComboBox.SelectedItem = Path.GetFileName(fs.pythonFlagSendScriptPath);
            }
        }

        private void threadUpdateScripts()
        {
            try
            {
                while (isDirectoryCheckForScriptsTHRMustRun)
                {
                    //GetScripts
                    if (!Directory.Exists("Python_Get_Scripts"))
                    {
                        Directory.CreateDirectory("Python_Get_Scripts");
                    }
                    var GetScripts = Directory.GetFiles("Python_Get_Scripts");

                    if (GetScripts.Length > 0)
                    {
                        foreach (var item in GetScripts)
                        {
                            if (!pythonGetScriptChooseComboBox.Items.Contains(Path.GetFileName(item)))
                            {
                                pythonGetScriptChooseComboBox.Items.Clear();

                                pythonGetScriptChooseComboBox.Items.Add("-Select Get Script-");
                                pythonGetScriptChooseComboBox.SelectedItem = "-Select Get Script-";

                                foreach (var script in GetScripts)
                                {
                                    pythonGetScriptChooseComboBox.Items.Add(Path.GetFileName(script));
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        pythonGetScriptChooseComboBox.Items.Clear();
                        pythonGetScriptChooseComboBox.Items.Add("-Select Get Script-");
                        pythonGetScriptChooseComboBox.SelectedItem = "-Select Get Script-";
                    }


                    //FlagSendScripts
                    if (!Directory.Exists("Python_FlagSend_Scripts"))
                    {
                        Directory.CreateDirectory("Python_FlagSend_Scripts");
                    }
                    var FlagSendScripts = Directory.GetFiles("Python_FlagSend_Scripts");

                    if (FlagSendScripts.Length > 0)
                    {
                        foreach (var item in FlagSendScripts)
                        {
                            if (!pythonFlagSendScriptChooseComboBox.Items.Contains(Path.GetFileName(item)))
                            {
                                pythonFlagSendScriptChooseComboBox.Items.Clear();

                                pythonFlagSendScriptChooseComboBox.Items.Add("-Select FlagSend Script-");
                                pythonFlagSendScriptChooseComboBox.SelectedItem = "-Select FlagSend Script-";

                                foreach (var script in FlagSendScripts)
                                {
                                    pythonFlagSendScriptChooseComboBox.Items.Add(Path.GetFileName(script));
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        pythonFlagSendScriptChooseComboBox.Items.Clear();
                        pythonFlagSendScriptChooseComboBox.Items.Add("-Select FlagSend Script-");
                        pythonFlagSendScriptChooseComboBox.SelectedItem = "-Select FlagSend Script-";
                    }

                    Thread.Sleep(5000);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Warning!\nSome problem while reading scripts from directory{exp}", "WARNING");
            }
        }


        #region Button EventHandlers
        private void deployButton_Enter(object? sender, EventArgs e)
        {
            deployButton.BackColor = Color.Purple;
        }
        private void deployButton_Leave(object? sender, EventArgs e)
        {
            deployButton.BackColor = Color.Black;
        }
        #endregion

        private void deployButton_Click(object sender, EventArgs e)
        {
            noFlagFormatLabel.Visible = false;
            noTeamTokenLabel.Visible = false;
            noTeamOwnerIPLabel.Visible = false;
            noRoundTimeLabel.Visible = false;
            noFlagLifeTimeLabel.Visible = false;
            noScoreboardURLLabel.Visible = false;
            noFlagsubmitterURLLabel.Visible = false;

            noPythonGetScriptLabel.Visible = false;
            noPythonFlagSendScriptLabel.Visible = false;

            if (string.IsNullOrWhiteSpace(flagFormatTextBox.Text) || flagFormatTextBox.Text == ">Regex flag format")
            {
                noFlagFormatLabel.Visible = true;
            }
            try
            {
                Regex reg = new Regex(flagFormatTextBox.Text);
            }
            catch
            {
                flagFormatTextBox.Text = "";
                noFlagFormatLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(teamTokenTextBox.Text) || teamTokenTextBox.Text == ">X-Team-Token")
            {
                noTeamTokenLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(teamOwnerIPTextBox.Text) || teamOwnerIPTextBox.Text == ">Your Team IP")
            {
                noTeamOwnerIPLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(roundTimeTextBox.Text) || roundTimeTextBox.Text == ">Round Time (in sec)")
            {
                noRoundTimeLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(flagLifeTimeTextBox.Text) || flagLifeTimeTextBox.Text == ">Flag Lifetime(in rounds)")
            {
                noFlagLifeTimeLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(scoreBoardURLTextBox.Text) || scoreBoardURLTextBox.Text == ">Flag Submitter URL")
            {
                noScoreboardURLLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(flagSubmitterURLTextBox.Text) || flagSubmitterURLTextBox.Text == ">Flag Submitter URL")
            {
                noFlagsubmitterURLLabel.Visible = true;
            }
            if (pythonGetScriptChooseComboBox.SelectedIndex == 0)
            {
                noPythonGetScriptLabel.Visible = true;
            }
            if (pythonFlagSendScriptChooseComboBox.SelectedIndex == 0)
            {
                noPythonFlagSendScriptLabel.Visible = true;
            }

            if(noFlagFormatLabel.Visible == true || noTeamTokenLabel.Visible == true || noTeamOwnerIPLabel.Visible == true || 
                noRoundTimeLabel.Visible == true || noFlagLifeTimeLabel.Visible == true || noScoreboardURLLabel.Visible == true ||
                noFlagsubmitterURLLabel.Visible == true || noPythonGetScriptLabel.Visible == true || noPythonFlagSendScriptLabel.Visible == true)
            {
                return;
            }

            if (ifClearLastWorkCheckBox.Checked)
            {
                if (MessageBox.Show("Attention!\nYou are trying to delete past data from the database.\n\nDo you really want to delete this data?", "ATTENTION", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DBWorkForm.DropTableInDB(new CTFTeam());
                    DBWorkForm.DropTableInDB(new FlagHistory());

                    DBWorkForm.CreateTableByClass(new CTFTeam());
                    DBWorkForm.CreateTableByClass(new FlagHistory());
                }
                else
                {
                    return;
                }
            }

            //if all the checks have passed -> deploy farm with saving settings
            isDirectoryCheckForScriptsTHRMustRun = false;

            MessageBox.Show(Directory.GetCurrentDirectory() + "\\Python_Get_Scripts\\" + pythonGetScriptChooseComboBox.SelectedItem);
            MessageBox.Show(Directory.GetCurrentDirectory() + "\\Python_FlagSend_Scripts\\" + pythonFlagSendScriptChooseComboBox.SelectedItem);

            this.fs = new FarmSettings(flagFormatTextBox.Text, teamTokenTextBox.Text, teamOwnerIPTextBox.Text, Convert.ToInt32(roundTimeTextBox.Text) * 1000, Convert.ToInt32(flagLifeTimeTextBox.Text), scoreBoardURLTextBox.Text, flagSubmitterURLTextBox.Text, Directory.GetCurrentDirectory() + "\\Python_Get_Scripts\\" + pythonGetScriptChooseComboBox.SelectedItem, Directory.GetCurrentDirectory() + "\\Python_FlagSend_Scripts\\" + pythonFlagSendScriptChooseComboBox.SelectedItem);
            MainForm mainForm = new MainForm(fs, DBWorkForm);
            this.Hide();
            mainForm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed);
            mainForm.ShowDialog();
        }

        private void mainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            this.Show();

            isDirectoryCheckForScriptsTHRMustRun = true;
            DirectoryCheckForScriptsTHR = new Thread(threadUpdateScripts);
            DirectoryCheckForScriptsTHR.Start();
        }

        #region TextBoxs
        private void CursorMove_TextBoxs(TextBox textBox, int pos)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                textBox.Select(pos, 0);
            });
        }

        private void flagFormatTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void teamTokenTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void teamOwnerIPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (teamOwnerIPTextBox.Text != ">Your Team IP")
            {
                Regex regex = new Regex(@"[\d]|\.");
                foreach (var simb in teamOwnerIPTextBox.Text)
                {
                    if (!regex.IsMatch(Convert.ToString(simb)))
                    {
                        teamOwnerIPTextBox.Text = teamOwnerIPTextBox.Text.Replace(Convert.ToString(simb), string.Empty);
                        CursorMove_TextBoxs(teamOwnerIPTextBox, teamOwnerIPTextBox.Text.Length);
                    }
                }
            }
        }
        private void flagLifeTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (flagLifeTimeTextBox.Text != ">Flag Lifetime(in rounds)")
            {
                Regex regex = new Regex(@"[\d]");
                foreach (var simb in flagLifeTimeTextBox.Text)
                {
                    if (!regex.IsMatch(Convert.ToString(simb)))
                    {
                        flagLifeTimeTextBox.Text = flagLifeTimeTextBox.Text.Replace(Convert.ToString(simb), string.Empty);
                        CursorMove_TextBoxs(flagLifeTimeTextBox, flagLifeTimeTextBox.Text.Length);
                    }
                }
            }
        }
        private void roundTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (roundTimeTextBox.Text != ">Round Time (in sec)")
            {
                Regex regex = new Regex(@"[\d]");
                foreach (var simb in roundTimeTextBox.Text)
                {
                    if (!regex.IsMatch(Convert.ToString(simb)))
                    {
                        roundTimeTextBox.Text = roundTimeTextBox.Text.Replace(Convert.ToString(simb), string.Empty);
                        CursorMove_TextBoxs(roundTimeTextBox, roundTimeTextBox.Text.Length);
                    }
                }
            }
        }
        private void scoreBoardURLTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void flagSubmitterURLTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region TextBoxs EventHandlers
        private void flagSubmitterURLTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(flagSubmitterURLTextBox.Text))
            {
                flagSubmitterURLTextBox.Text = ">Flag Submitter URL";
                flagSubmitterURLTextBox.ForeColor = Color.Gray;
            }
        }
        private void flagSubmitterURLTextBox_Enter(object? sender, EventArgs e)
        {
            if (flagSubmitterURLTextBox.Text == ">Flag Submitter URL")
            {
                flagSubmitterURLTextBox.Clear();
                flagSubmitterURLTextBox.ForeColor = Color.Black;
            }
        }

        private void scoreBoardURLTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(scoreBoardURLTextBox.Text))
            {
                scoreBoardURLTextBox.Text = ">Scoreboard URL";
                scoreBoardURLTextBox.ForeColor = Color.Gray;
            }
        }
        private void scoreBoardURLTextBox_Enter(object? sender, EventArgs e)
        {
            if (scoreBoardURLTextBox.Text == ">Scoreboard URL")
            {
                scoreBoardURLTextBox.Clear();
                scoreBoardURLTextBox.ForeColor = Color.Black;
            }
        }


        private void roundTimeTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(roundTimeTextBox.Text))
            {
                roundTimeTextBox.Text = ">Round Time (in sec)";
                roundTimeTextBox.ForeColor = Color.Gray;
            }
        }
        private void roundTimeTextBox_Enter(object? sender, EventArgs e)
        {
            if (roundTimeTextBox.Text == ">Round Time (in sec)")
            {
                roundTimeTextBox.Clear();
                roundTimeTextBox.ForeColor = Color.Black;
            }
        }

        private void flagLifeTimeTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(flagLifeTimeTextBox.Text))
            {
                flagLifeTimeTextBox.Text = ">Flag Lifetime(in rounds)";
                flagLifeTimeTextBox.ForeColor = Color.Gray;
            }
        }
        private void flagLifeTimeTextBox_Enter(object? sender, EventArgs e)
        {
            if (flagLifeTimeTextBox.Text == ">Flag Lifetime(in rounds)")
            {
                flagLifeTimeTextBox.Clear();
                flagLifeTimeTextBox.ForeColor = Color.Black;
            }
        }


        private void teamOwnerIPTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teamOwnerIPTextBox.Text))
            {
                teamOwnerIPTextBox.Text = ">Your Team IP";
                teamOwnerIPTextBox.ForeColor = Color.Gray;
            }
        }
        private void teamOwnerIPTextBox_Enter(object? sender, EventArgs e)
        {
            if (teamOwnerIPTextBox.Text == ">Your Team IP")
            {
                teamOwnerIPTextBox.Clear();
                teamOwnerIPTextBox.ForeColor = Color.Black;
            }
        }


        private void teamTokenTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teamTokenTextBox.Text))
            {
                teamTokenTextBox.Text = ">X-Team-Token";
                teamTokenTextBox.ForeColor = Color.Gray;
            }
        }
        private void teamTokenTextBox_Enter(object? sender, EventArgs e)
        {
            if (teamTokenTextBox.Text == ">X-Team-Token")
            {
                teamTokenTextBox.Clear();
                teamTokenTextBox.ForeColor = Color.Black;
            }
        }


        private void flagFormatTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(flagFormatTextBox.Text))
            {
                flagFormatTextBox.Text = ">Regex flag format";
                flagFormatTextBox.ForeColor = Color.Gray;
            }
        }
        private void flagFormatTextBox_Enter(object? sender, EventArgs e)
        {
            if (flagFormatTextBox.Text == ">Regex flag format")
            {
                flagFormatTextBox.Clear();
                flagFormatTextBox.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Labels
        private void PresetSettingsLabel_Click(object sender, EventArgs e)
        {

        }
        #region Except Info Labels
        private void noFlagFormatLabel_Click(object sender, EventArgs e)
        {

        }
        private void noTeamTokenLabel_Click(object sender, EventArgs e)
        {

        }
        private void noTeamOwnerIPLabel_Click(object sender, EventArgs e)
        {

        }
        private void noRoundTimeLabel_Click(object sender, EventArgs e)
        {

        }
        private void noFlagLifeTimeLabel_Click(object sender, EventArgs e)
        {

        }
        private void noScoreboardURLLabel_Click(object sender, EventArgs e)
        {

        }
        private void noFlagsubmitterURLLabel_Click(object sender, EventArgs e)
        {

        }

        private void noPythonGetScriptLabel_Click(object sender, EventArgs e)
        {

        }
        private void noPythonFlagSendScriptLabel_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        private void ifClearLastWorkCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pythonGetScriptChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void pythonFlagSendScriptChooseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
