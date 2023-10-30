namespace VQTFarm
{
    partial class ManualTeamAddForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualTeamAddForm));
            teamIPTextBox = new TextBox();
            teamNameTextBox = new TextBox();
            AddButton = new Button();
            noTeamNameLabel = new Label();
            noTeamIPLabel = new Label();
            SuspendLayout();
            // 
            // teamIPTextBox
            // 
            teamIPTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            teamIPTextBox.Location = new Point(12, 63);
            teamIPTextBox.Name = "teamIPTextBox";
            teamIPTextBox.Size = new Size(229, 30);
            teamIPTextBox.TabIndex = 2;
            teamIPTextBox.TextChanged += teamIPTextBox_TextChanged;
            // 
            // teamNameTextBox
            // 
            teamNameTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            teamNameTextBox.Location = new Point(12, 12);
            teamNameTextBox.Name = "teamNameTextBox";
            teamNameTextBox.Size = new Size(229, 30);
            teamNameTextBox.TabIndex = 1;
            teamNameTextBox.TextChanged += teamNameTextBox_TextChanged;
            // 
            // AddButton
            // 
            AddButton.BackColor = SystemColors.WindowText;
            AddButton.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            AddButton.ForeColor = Color.Lime;
            AddButton.Location = new Point(247, 27);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(162, 66);
            AddButton.TabIndex = 3;
            AddButton.Text = "Add Team";
            AddButton.UseVisualStyleBackColor = false;
            AddButton.Click += AddButton_Click;
            // 
            // noTeamNameLabel
            // 
            noTeamNameLabel.AutoSize = true;
            noTeamNameLabel.ForeColor = Color.Red;
            noTeamNameLabel.Location = new Point(12, 45);
            noTeamNameLabel.Name = "noTeamNameLabel";
            noTeamNameLabel.Size = new Size(97, 15);
            noTeamNameLabel.TabIndex = 11;
            noTeamNameLabel.Text = "Enter team name";
            noTeamNameLabel.Click += noTeamNameLabel_Click;
            // 
            // noTeamIPLabel
            // 
            noTeamIPLabel.AutoSize = true;
            noTeamIPLabel.ForeColor = Color.Red;
            noTeamIPLabel.Location = new Point(12, 96);
            noTeamIPLabel.Name = "noTeamIPLabel";
            noTeamIPLabel.Size = new Size(77, 15);
            noTeamIPLabel.TabIndex = 12;
            noTeamIPLabel.Text = "Enter team IP";
            noTeamIPLabel.Click += noTeamIPLabel_Click;
            // 
            // ManualTeamAddForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkBlue;
            ClientSize = new Size(421, 120);
            Controls.Add(noTeamIPLabel);
            Controls.Add(noTeamNameLabel);
            Controls.Add(AddButton);
            Controls.Add(teamNameTextBox);
            Controls.Add(teamIPTextBox);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ManualTeamAddForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add new team";
            Load += ManualTeamAddForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox teamIPTextBox;
        private TextBox teamNameTextBox;
        private Button AddButton;
        private Label noTeamNameLabel;
        private Label noTeamIPLabel;
    }
}