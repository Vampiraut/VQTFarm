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
            noAdminServerIPLabel.Visible = false;

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

            adminServerIPTextBox.Enter += new EventHandler(adminServerIPTextBox_Enter);
            adminServerIPTextBox.Leave += new EventHandler(adminServerIPTextBox_Leave);
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

            adminServerIPTextBox.Text = ">Flag Submitter URL";
            adminServerIPTextBox.ForeColor = Color.Gray;
        }
        private void deployButton_Click(object sender, EventArgs e)
        {
            noFlagFormatLabel.Visible = false;
            noTeamTokenLabel.Visible = false;
            noTeamOwnerIPLabel.Visible = false;
            noRoundTimeLabel.Visible = false;
            noAdminServerIPLabel.Visible = false;

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
            if (string.IsNullOrWhiteSpace(adminServerIPTextBox.Text) || adminServerIPTextBox.Text == ">Flag Submitter URL")
            {
                noAdminServerIPLabel.Visible = true;
                return;
            }

            FarmSettings fs = new FarmSettings(new Regex(flagFormatTextBox.Text), teamTokenTextBox.Text, teamOwnerIPTextBox.Text, Convert.ToInt32(roundTimeTextBox.Text) * 1000, adminServerIPTextBox.Text);
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
        private void adminServerIPTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region TextBoxs EventHandlers
        private void adminServerIPTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(adminServerIPTextBox.Text))
            {
                adminServerIPTextBox.Text = ">Flag Submitter URL";
                adminServerIPTextBox.ForeColor = Color.Gray;
            }
        }
        private void adminServerIPTextBox_Enter(object? sender, EventArgs e)
        {
            if (adminServerIPTextBox.Text == ">Flag Submitter URL")
            {
                adminServerIPTextBox.Clear();
                adminServerIPTextBox.ForeColor = Color.Black;
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

        private void noAdminServerIPLabel_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion

        private void ifClearLastWorkCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
