namespace VQTFarm
{
    partial class PresetStartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetStartForm));
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            PresetSettingsLabel = new Label();
            flagFormatTextBox = new TextBox();
            teamTokenTextBox = new TextBox();
            teamOwnerIPTextBox = new TextBox();
            roundTimeTextBox = new TextBox();
            adminServerIPTextBox = new TextBox();
            deployButton = new Button();
            noFlagFormatLabel = new Label();
            noTeamTokenLabel = new Label();
            noTeamOwnerIPLabel = new Label();
            noRoundTimeLabel = new Label();
            noAdminServerIPLabel = new Label();
            ifClearLastWorkCheckBox = new CheckBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.Transparent;
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(363, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            settingsToolStripMenuItem.ForeColor = Color.Black;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(66, 21);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            helpToolStripMenuItem.ForeColor = Color.Black;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(47, 21);
            helpToolStripMenuItem.Text = "Help";
            // 
            // PresetSettingsLabel
            // 
            PresetSettingsLabel.AutoSize = true;
            PresetSettingsLabel.Font = new Font("Calibri", 20F, FontStyle.Bold, GraphicsUnit.Point);
            PresetSettingsLabel.ForeColor = Color.Indigo;
            PresetSettingsLabel.Location = new Point(12, 40);
            PresetSettingsLabel.Name = "PresetSettingsLabel";
            PresetSettingsLabel.Size = new Size(336, 33);
            PresetSettingsLabel.TabIndex = 1;
            PresetSettingsLabel.Text = "Preset Settings for VQT Farm";
            PresetSettingsLabel.Click += PresetSettingsLabel_Click;
            // 
            // flagFormatTextBox
            // 
            flagFormatTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            flagFormatTextBox.Location = new Point(12, 76);
            flagFormatTextBox.Name = "flagFormatTextBox";
            flagFormatTextBox.Size = new Size(339, 30);
            flagFormatTextBox.TabIndex = 2;
            flagFormatTextBox.TextChanged += flagFormatTextBox_TextChanged;
            // 
            // teamTokenTextBox
            // 
            teamTokenTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            teamTokenTextBox.Location = new Point(12, 127);
            teamTokenTextBox.Name = "teamTokenTextBox";
            teamTokenTextBox.Size = new Size(339, 30);
            teamTokenTextBox.TabIndex = 3;
            teamTokenTextBox.TextChanged += teamTokenTextBox_TextChanged;
            // 
            // teamOwnerIPTextBox
            // 
            teamOwnerIPTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            teamOwnerIPTextBox.Location = new Point(12, 178);
            teamOwnerIPTextBox.Name = "teamOwnerIPTextBox";
            teamOwnerIPTextBox.Size = new Size(339, 30);
            teamOwnerIPTextBox.TabIndex = 4;
            teamOwnerIPTextBox.TextChanged += teamOwnerIPTextBox_TextChanged;
            // 
            // roundTimeTextBox
            // 
            roundTimeTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            roundTimeTextBox.Location = new Point(12, 229);
            roundTimeTextBox.Name = "roundTimeTextBox";
            roundTimeTextBox.Size = new Size(339, 30);
            roundTimeTextBox.TabIndex = 5;
            roundTimeTextBox.TextChanged += roundTimeTextBox_TextChanged;
            // 
            // adminServerIPTextBox
            // 
            adminServerIPTextBox.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point);
            adminServerIPTextBox.Location = new Point(12, 280);
            adminServerIPTextBox.Name = "adminServerIPTextBox";
            adminServerIPTextBox.Size = new Size(339, 30);
            adminServerIPTextBox.TabIndex = 6;
            adminServerIPTextBox.TextChanged += adminServerIPTextBox_TextChanged;
            // 
            // deployButton
            // 
            deployButton.BackColor = Color.Black;
            deployButton.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            deployButton.ForeColor = Color.Red;
            deployButton.Location = new Point(12, 357);
            deployButton.Name = "deployButton";
            deployButton.Size = new Size(339, 54);
            deployButton.TabIndex = 7;
            deployButton.Text = "Deploy FARM";
            deployButton.UseVisualStyleBackColor = false;
            deployButton.Click += deployButton_Click;
            // 
            // noFlagFormatLabel
            // 
            noFlagFormatLabel.AutoSize = true;
            noFlagFormatLabel.ForeColor = Color.DarkRed;
            noFlagFormatLabel.Location = new Point(12, 109);
            noFlagFormatLabel.Name = "noFlagFormatLabel";
            noFlagFormatLabel.Size = new Size(96, 15);
            noFlagFormatLabel.TabIndex = 8;
            noFlagFormatLabel.Text = "Enter flag format";
            noFlagFormatLabel.Click += noFlagFormatLabel_Click;
            // 
            // noTeamTokenLabel
            // 
            noTeamTokenLabel.AutoSize = true;
            noTeamTokenLabel.ForeColor = Color.DarkRed;
            noTeamTokenLabel.Location = new Point(12, 160);
            noTeamTokenLabel.Name = "noTeamTokenLabel";
            noTeamTokenLabel.Size = new Size(97, 15);
            noTeamTokenLabel.TabIndex = 9;
            noTeamTokenLabel.Text = "Enter team token";
            noTeamTokenLabel.Click += noTeamTokenLabel_Click;
            // 
            // noTeamOwnerIPLabel
            // 
            noTeamOwnerIPLabel.AutoSize = true;
            noTeamOwnerIPLabel.ForeColor = Color.DarkRed;
            noTeamOwnerIPLabel.Location = new Point(12, 211);
            noTeamOwnerIPLabel.Name = "noTeamOwnerIPLabel";
            noTeamOwnerIPLabel.Size = new Size(104, 15);
            noTeamOwnerIPLabel.TabIndex = 10;
            noTeamOwnerIPLabel.Text = "Enter your team IP";
            noTeamOwnerIPLabel.Click += noTeamOwnerIPLabel_Click;
            // 
            // noRoundTimeLabel
            // 
            noRoundTimeLabel.AutoSize = true;
            noRoundTimeLabel.ForeColor = Color.DarkRed;
            noRoundTimeLabel.Location = new Point(12, 262);
            noRoundTimeLabel.Name = "noRoundTimeLabel";
            noRoundTimeLabel.Size = new Size(96, 15);
            noRoundTimeLabel.TabIndex = 11;
            noRoundTimeLabel.Text = "Enter round time";
            noRoundTimeLabel.Click += noRoundTimeLabel_Click;
            // 
            // noAdminServerIPLabel
            // 
            noAdminServerIPLabel.AutoSize = true;
            noAdminServerIPLabel.ForeColor = Color.DarkRed;
            noAdminServerIPLabel.Location = new Point(12, 313);
            noAdminServerIPLabel.Name = "noAdminServerIPLabel";
            noAdminServerIPLabel.Size = new Size(135, 15);
            noAdminServerIPLabel.TabIndex = 12;
            noAdminServerIPLabel.Text = "Enter flag submitter URL";
            noAdminServerIPLabel.Click += noAdminServerIPLabel_Click;
            // 
            // ifClearLastWorkCheckBox
            // 
            ifClearLastWorkCheckBox.AutoSize = true;
            ifClearLastWorkCheckBox.Font = new Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            ifClearLastWorkCheckBox.Location = new Point(12, 331);
            ifClearLastWorkCheckBox.Name = "ifClearLastWorkCheckBox";
            ifClearLastWorkCheckBox.Size = new Size(156, 20);
            ifClearLastWorkCheckBox.TabIndex = 13;
            ifClearLastWorkCheckBox.Text = "Clear Last info in DB";
            ifClearLastWorkCheckBox.UseVisualStyleBackColor = true;
            ifClearLastWorkCheckBox.CheckedChanged += ifClearLastWorkCheckBox_CheckedChanged;
            // 
            // PresetStartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DeepSkyBlue;
            ClientSize = new Size(363, 423);
            Controls.Add(ifClearLastWorkCheckBox);
            Controls.Add(noAdminServerIPLabel);
            Controls.Add(noRoundTimeLabel);
            Controls.Add(noTeamOwnerIPLabel);
            Controls.Add(noTeamTokenLabel);
            Controls.Add(noFlagFormatLabel);
            Controls.Add(deployButton);
            Controls.Add(adminServerIPTextBox);
            Controls.Add(roundTimeTextBox);
            Controls.Add(teamOwnerIPTextBox);
            Controls.Add(teamTokenTextBox);
            Controls.Add(flagFormatTextBox);
            Controls.Add(PresetSettingsLabel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "PresetStartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Preset Settings for VQT Farm";
            Load += PresetStartForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private Label PresetSettingsLabel;
        private TextBox flagFormatTextBox;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TextBox teamTokenTextBox;
        private TextBox teamOwnerIPTextBox;
        private TextBox roundTimeTextBox;
        private TextBox adminServerIPTextBox;
        private Button deployButton;
        private Label noFlagFormatLabel;
        private Label noTeamTokenLabel;
        private Label noTeamOwnerIPLabel;
        private Label noRoundTimeLabel;
        private Label noAdminServerIPLabel;
        private CheckBox ifClearLastWorkCheckBox;
    }
}