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
    public partial class AutoTeamsGenerationForm : Form
    {
        public AutoTeamsGenerationForm()
        {
            InitializeComponent();
        }
        private void AutoTeamsGenerationForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = 999;
            numericUpDown1.Minimum = 0;

            numericUpDown2.Maximum = 999;
            numericUpDown2.Minimum = 0;

            numericUpDown3.Maximum = 999;
            numericUpDown3.Minimum = 0;

            numericUpDown4.Maximum = 999;
            numericUpDown4.Minimum = 0;

            numericUpDown5.Maximum = 999;
            numericUpDown5.Minimum = 0;

            numericUpDown6.Maximum = 999;
            numericUpDown6.Minimum = 0;

            numericUpDown7.Maximum = 999;
            numericUpDown7.Minimum = 0;

            numericUpDown8.Maximum = 999;
            numericUpDown8.Minimum = 0;

            defaultNameTextBox.Text = ">Default name";
            defaultNameTextBox.ForeColor = Color.Gray;
            defaultNameTextBox.Enter += new EventHandler(defaultNameTextBox_Enter);
            defaultNameTextBox.Leave += new EventHandler(defaultNameTextBox_Leave);

            startGenerationButton.MouseEnter += new EventHandler(startGenerationButton_Enter);
            startGenerationButton.MouseLeave += new EventHandler(startGenerationButton_Leave);

            warningMesLabel.Visible = false;
        }
        private void defaultNameTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(defaultNameTextBox.Text))
            {
                defaultNameTextBox.Text = ">Default name";
                defaultNameTextBox.ForeColor = Color.Gray;
            }
        }

        private void defaultNameTextBox_Enter(object? sender, EventArgs e)
        {
            if (defaultNameTextBox.Text == ">Default name")
            {
                defaultNameTextBox.Clear();
                defaultNameTextBox.ForeColor = Color.Black;
            }
        }

        #region NumericUpDown
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }
        #endregion
        private void defaultNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void startGenerationButton_Click(object sender, EventArgs e)
        {
            if (!((numericUpDown1.Value != numericUpDown5.Value && numericUpDown2.Value == numericUpDown6.Value && numericUpDown3.Value == numericUpDown7.Value && numericUpDown4.Value == numericUpDown8.Value) ||
                (numericUpDown1.Value == numericUpDown5.Value && numericUpDown2.Value != numericUpDown6.Value && numericUpDown3.Value == numericUpDown7.Value && numericUpDown4.Value == numericUpDown8.Value) ||
                (numericUpDown1.Value == numericUpDown5.Value && numericUpDown2.Value == numericUpDown6.Value && numericUpDown3.Value != numericUpDown7.Value && numericUpDown4.Value == numericUpDown8.Value) ||
                (numericUpDown1.Value == numericUpDown5.Value && numericUpDown2.Value == numericUpDown6.Value && numericUpDown3.Value == numericUpDown7.Value && numericUpDown4.Value != numericUpDown8.Value)))
            {
                warningMesLabel.Visible = true;
                return;
            }
            warningMesLabel.Visible = false;

            DataBaseWorkAplication DBWorkTeamsAdd = new DataBaseWorkAplication();
            DBWorkTeamsAdd.StartConnection("Data Source=FarmInfo.db");

            if (numericUpDown1.Value != numericUpDown5.Value)
            {
                for (int i = Convert.ToInt32(numericUpDown1.Value); i <= Convert.ToInt32(numericUpDown5.Value); i++)
                {
                    DBWorkTeamsAdd.AddClassToDB(new CTFTeam("none", (defaultNameTextBox.Text != ">Default name" ? defaultNameTextBox.Text : "SomeTeam") + Convert.ToString(i), $"{i}.{numericUpDown2.Value}.{numericUpDown3.Value}.{numericUpDown4.Value}", "none"));
                }
            }
            else if (numericUpDown2.Value != numericUpDown6.Value)
            {
                for (int i = Convert.ToInt32(numericUpDown2.Value); i <= Convert.ToInt32(numericUpDown6.Value); i++)
                {
                    DBWorkTeamsAdd.AddClassToDB(new CTFTeam("none", (defaultNameTextBox.Text != ">Default name" ? defaultNameTextBox.Text : "SomeTeam") + Convert.ToString(i), $"{numericUpDown1.Value}.{i}.{numericUpDown3.Value}.{numericUpDown4.Value}", "none"));
                }
            }
            else if (numericUpDown3.Value != numericUpDown7.Value)
            {
                for (int i = Convert.ToInt32(numericUpDown3.Value); i <= Convert.ToInt32(numericUpDown7.Value); i++)
                {
                    DBWorkTeamsAdd.AddClassToDB(new CTFTeam("none", (defaultNameTextBox.Text != ">Default name" ? defaultNameTextBox.Text : "SomeTeam") + Convert.ToString(i), $"{numericUpDown1.Value}.{numericUpDown2.Value}.{i}.{numericUpDown4.Value}", "none"));
                }
            }
            else if (numericUpDown4.Value != numericUpDown8.Value)
            {
                for (int i = Convert.ToInt32(numericUpDown4.Value); i <= Convert.ToInt32(numericUpDown8.Value); i++)
                {
                    DBWorkTeamsAdd.AddClassToDB(new CTFTeam("none", (defaultNameTextBox.Text != ">Default name" ? defaultNameTextBox.Text : "SomeTeam") + Convert.ToString(i), $"{numericUpDown1.Value}.{numericUpDown2.Value}.{numericUpDown3.Value}.{i}", "none"));
                }
            }
            this.Close();
        }

        private void warningMesLabel_Click(object sender, EventArgs e)
        {

        }

        #region Button EventHandlers
        private void startGenerationButton_Enter(object? sender, EventArgs e)
        {
            startGenerationButton.BackColor = Color.Purple;
        }
        private void startGenerationButton_Leave(object? sender, EventArgs e)
        {
            startGenerationButton.BackColor = Color.Black;
        }
        #endregion
    }
}
