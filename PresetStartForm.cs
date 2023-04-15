using System.Text.RegularExpressions;

namespace VQTFarm
{
    public partial class PresetStartForm : Form
    {
        DataBaseWorkAplication DBWorkForm = new DataBaseWorkAplication();
        public PresetStartForm()
        {
            DBWorkForm.StartConnection("Data Source=FarmInfo.db");
            DBWorkForm.CreateTableByClass(new CTFTeam());
            DBWorkForm.CreateTableByClass(new FlagHistory());

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

            #endregion

            StartGhostTextSet();
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

        public void StartGhostTextSet()
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
        private void deployButton_Click(object sender, EventArgs e)
        {
            noFlagFormatLabel.Visible = false;
            noTeamTokenLabel.Visible = false;
            noTeamOwnerIPLabel.Visible = false;
            noRoundTimeLabel.Visible = false;
            noFlagLifeTimeLabel.Visible = false;
            noScoreboardURLLabel.Visible = false;
            noFlagsubmitterURLLabel.Visible = false;

            if (string.IsNullOrWhiteSpace(flagFormatTextBox.Text) || flagFormatTextBox.Text == ">Regex flag format")
            {
                noFlagFormatLabel.Visible = true;
                return;
            }

            try
            {
                Regex reg = new Regex(flagFormatTextBox.Text);
            }
            catch
            {
                flagFormatTextBox.Text = "";
                noFlagFormatLabel.Visible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(teamTokenTextBox.Text) || teamTokenTextBox.Text == ">X-Team-Token")
            {
                noTeamTokenLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(teamOwnerIPTextBox.Text) || teamOwnerIPTextBox.Text == ">Your Team IP")
            {
                noTeamOwnerIPLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(roundTimeTextBox.Text) || roundTimeTextBox.Text == ">Round Time (in sec)")
            {
                noRoundTimeLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(flagLifeTimeTextBox.Text) || flagLifeTimeTextBox.Text == ">Flag Lifetime(in rounds)")
            {
                noFlagLifeTimeLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(scoreBoardURLTextBox.Text) || scoreBoardURLTextBox.Text == ">Flag Submitter URL")
            {
                noScoreboardURLLabel.Visible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(flagSubmitterURLTextBox.Text) || flagSubmitterURLTextBox.Text == ">Flag Submitter URL")
            {
                noFlagsubmitterURLLabel.Visible = true;
                return;
            }

            if(ifClearLastWorkCheckBox.Checked)
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

            FarmSettings fs = new FarmSettings(new Regex(flagFormatTextBox.Text), teamTokenTextBox.Text, teamOwnerIPTextBox.Text, Convert.ToInt32(roundTimeTextBox.Text) * 1000, Convert.ToInt32(flagLifeTimeTextBox.Text), scoreBoardURLTextBox.Text, flagSubmitterURLTextBox.Text);
            MainForm mainForm = new MainForm(fs, DBWorkForm);
            this.Hide();
            mainForm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed);
            mainForm.ShowDialog();
        }

        private void mainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            this.Show();
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
        #endregion
        #endregion

        private void ifClearLastWorkCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
