using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

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

            if(noTeamNameLabel.Visible == true || noTeamIPLabel.Visible == true)
            {
                return;
            }

            DataBaseWorkAplication dbWork = new DataBaseWorkAplication();
            dbWork.StartConnection("Data Source=FarmInfo.db");

            dbWork.AddClassToDB(new CTFTeam("none", teamNameTextBox.Text, teamIPTextBox.Text, "none"));

            this.Close();
        }

        private void teamNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void teamIPTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void noTeamNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void noTeamIPLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
