using System.Text.RegularExpressions;

namespace VQTFarm
{
    public partial class ManualTeamAddForm : Form
    {
        public ManualTeamAddForm()
        {
            InitializeComponent();
        }
        private void ManualTeamAddForm_Load(object sender, EventArgs e)
        {
            AddButton.Select();

            noTeamIPLabel.Visible = false;
            noTeamNameLabel.Visible = false;

            teamNameTextBox.Text = ">Team Name";
            teamNameTextBox.ForeColor = Color.Gray;

            teamIPTextBox.Text = ">Team IP";
            teamIPTextBox.ForeColor = Color.Gray;

            teamNameTextBox.Enter += new EventHandler(teamNameTextBox_Enter);
            teamNameTextBox.Leave += new EventHandler(teamNameTextBox_Leave);

            teamIPTextBox.Enter += new EventHandler(teamIPTextBox_Enter);
            teamIPTextBox.Leave += new EventHandler(teamIPTextBox_Leave);

            AddButton.MouseEnter += new EventHandler(AddButton_Enter);
            AddButton.MouseLeave += new EventHandler(AddButton_Leave);
        }

        #region TextBoxs EventHandlers
        private void teamIPTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teamIPTextBox.Text))
            {
                teamIPTextBox.Text = ">Team IP";
                teamIPTextBox.ForeColor = Color.Gray;
            }
        }
        private void teamIPTextBox_Enter(object? sender, EventArgs e)
        {
            if (teamIPTextBox.Text == ">Team IP")
            {
                teamIPTextBox.Clear();
                teamIPTextBox.ForeColor = Color.Black;
            }
        }

        private void teamNameTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(teamNameTextBox.Text))
            {
                teamNameTextBox.Text = ">Team Name";
                teamNameTextBox.ForeColor = Color.Gray;
            }
        }
        private void teamNameTextBox_Enter(object? sender, EventArgs e)
        {
            if (teamNameTextBox.Text == ">Team Name")
            {
                teamNameTextBox.Clear();
                teamNameTextBox.ForeColor = Color.Black;
            }
        }
        #endregion

        private void AddButton_Click(object sender, EventArgs e)
        {
            noTeamIPLabel.Visible = false;
            noTeamNameLabel.Visible = false;

            if (string.IsNullOrWhiteSpace(teamNameTextBox.Text) || teamNameTextBox.Text == ">Team Name")
            {
                noTeamNameLabel.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(teamIPTextBox.Text) || teamIPTextBox.Text == ">Team IP")
            {
                noTeamIPLabel.Visible = true;
            }

            if (noTeamNameLabel.Visible == true || noTeamIPLabel.Visible == true)
            {
                return;
            }

            DataBaseWorkAplication dbWork = new DataBaseWorkAplication();
            dbWork.StartConnection("Data Source=FarmInfo.db");

            dbWork.AddClassToDB(new CTFTeam("none", teamNameTextBox.Text, teamIPTextBox.Text, "none"));

            this.Close();
        }

        #region TextBoxs
        private void CursorMove_TextBoxs(TextBox textBox, int pos)
        {
            this.BeginInvoke((MethodInvoker)delegate ()
            {
                textBox.Select(pos, 0);
            });
        }
        private void teamNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void teamIPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (teamIPTextBox.Text != ">Team IP")
            {
                Regex regex = new Regex(@"[\d]|\.");
                foreach (var simb in teamIPTextBox.Text)
                {
                    if (!regex.IsMatch(Convert.ToString(simb)))
                    {
                        teamIPTextBox.Text = teamIPTextBox.Text.Replace(Convert.ToString(simb), string.Empty);
                        CursorMove_TextBoxs(teamIPTextBox, teamIPTextBox.Text.Length);
                    }
                }
            }
        }
        #endregion
        #region Labels
        private void noTeamNameLabel_Click(object sender, EventArgs e)
        {

        }
        private void noTeamIPLabel_Click(object sender, EventArgs e)
        {

        }
        #endregion



        #region Button EventHandlers
        private void AddButton_Enter(object? sender, EventArgs e)
        {
            AddButton.BackColor = Color.Purple;
        }
        private void AddButton_Leave(object? sender, EventArgs e)
        {
            AddButton.BackColor = Color.Black;
        }
        #endregion
    }
}
