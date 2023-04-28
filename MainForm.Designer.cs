namespace VQTFarm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            teamsToolStripMenuItem = new ToolStripMenuItem();
            flagHistoryToolStripMenuItem = new ToolStripMenuItem();
            manualSubmitToolStripMenuItem = new ToolStripMenuItem();
            exploitTestToolStripMenuItem = new ToolStripMenuItem();
            flagShowFilterToolStripMenuItem = new ToolStripMenuItem();
            addTeamManualToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            flagStatusGridView = new DataGridView();
            exploitTestPanel = new Panel();
            exploitChooseTextBox = new TextBox();
            runTestButton = new Button();
            teamChooseComboBox = new ComboBox();
            exploitTestPanelLabel = new Label();
            flagStatusLabel = new Label();
            manualSubmitPanel = new Panel();
            manualSubmitButton = new Button();
            manualSubmitTextBox = new TextBox();
            manualFlagSubmitPanelLabel = new Label();
            flagTotalAceptedLabel = new Label();
            flagShowFilterPanel = new Panel();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            flagShowFilterPanelLabel = new Label();
            teamsPlaceDataGridView = new DataGridView();
            teamsListPanel = new Panel();
            AutoTeamsParsFromScoreBoardCheckBox = new CheckBox();
            teamsListLabel = new Label();
            flagStatusPanel = new Panel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)flagStatusGridView).BeginInit();
            exploitTestPanel.SuspendLayout();
            manualSubmitPanel.SuspendLayout();
            flagShowFilterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)teamsPlaceDataGridView).BeginInit();
            teamsListPanel.SuspendLayout();
            flagStatusPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1256, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { showToolStripMenuItem, addTeamManualToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { teamsToolStripMenuItem, flagHistoryToolStripMenuItem, manualSubmitToolStripMenuItem, exploitTestToolStripMenuItem, flagShowFilterToolStripMenuItem });
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(169, 22);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // teamsToolStripMenuItem
            // 
            teamsToolStripMenuItem.Checked = true;
            teamsToolStripMenuItem.CheckState = CheckState.Checked;
            teamsToolStripMenuItem.Name = "teamsToolStripMenuItem";
            teamsToolStripMenuItem.Size = new Size(157, 22);
            teamsToolStripMenuItem.Text = "Teams";
            teamsToolStripMenuItem.Click += teamsToolStripMenuItem_Click;
            // 
            // flagHistoryToolStripMenuItem
            // 
            flagHistoryToolStripMenuItem.Checked = true;
            flagHistoryToolStripMenuItem.CheckState = CheckState.Checked;
            flagHistoryToolStripMenuItem.Name = "flagHistoryToolStripMenuItem";
            flagHistoryToolStripMenuItem.Size = new Size(157, 22);
            flagHistoryToolStripMenuItem.Text = "Flag History";
            flagHistoryToolStripMenuItem.Click += flagHistoryToolStripMenuItem_Click;
            // 
            // manualSubmitToolStripMenuItem
            // 
            manualSubmitToolStripMenuItem.Checked = true;
            manualSubmitToolStripMenuItem.CheckState = CheckState.Checked;
            manualSubmitToolStripMenuItem.Name = "manualSubmitToolStripMenuItem";
            manualSubmitToolStripMenuItem.Size = new Size(157, 22);
            manualSubmitToolStripMenuItem.Text = "Manual Submit";
            manualSubmitToolStripMenuItem.Click += manualSubmitToolStripMenuItem_Click;
            // 
            // exploitTestToolStripMenuItem
            // 
            exploitTestToolStripMenuItem.Checked = true;
            exploitTestToolStripMenuItem.CheckState = CheckState.Checked;
            exploitTestToolStripMenuItem.Name = "exploitTestToolStripMenuItem";
            exploitTestToolStripMenuItem.Size = new Size(157, 22);
            exploitTestToolStripMenuItem.Text = "Exploit Test";
            exploitTestToolStripMenuItem.Click += exploitTestToolStripMenuItem_Click;
            // 
            // flagShowFilterToolStripMenuItem
            // 
            flagShowFilterToolStripMenuItem.Checked = true;
            flagShowFilterToolStripMenuItem.CheckState = CheckState.Checked;
            flagShowFilterToolStripMenuItem.Name = "flagShowFilterToolStripMenuItem";
            flagShowFilterToolStripMenuItem.Size = new Size(157, 22);
            flagShowFilterToolStripMenuItem.Text = "Flag Show Filter";
            flagShowFilterToolStripMenuItem.Click += flagShowFilterToolStripMenuItem_Click;
            // 
            // addTeamManualToolStripMenuItem
            // 
            addTeamManualToolStripMenuItem.Name = "addTeamManualToolStripMenuItem";
            addTeamManualToolStripMenuItem.Size = new Size(169, 22);
            addTeamManualToolStripMenuItem.Text = "Add team manual";
            addTeamManualToolStripMenuItem.Click += addTeamManualToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // flagStatusGridView
            // 
            flagStatusGridView.AllowUserToAddRows = false;
            flagStatusGridView.AllowUserToDeleteRows = false;
            flagStatusGridView.AllowUserToResizeColumns = false;
            flagStatusGridView.AllowUserToResizeRows = false;
            flagStatusGridView.BackgroundColor = SystemColors.Window;
            flagStatusGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            flagStatusGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            flagStatusGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            flagStatusGridView.DefaultCellStyle = dataGridViewCellStyle2;
            flagStatusGridView.Location = new Point(3, 35);
            flagStatusGridView.MultiSelect = false;
            flagStatusGridView.Name = "flagStatusGridView";
            flagStatusGridView.ReadOnly = true;
            flagStatusGridView.RowTemplate.Height = 25;
            flagStatusGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            flagStatusGridView.ShowCellErrors = false;
            flagStatusGridView.ShowCellToolTips = false;
            flagStatusGridView.ShowEditingIcon = false;
            flagStatusGridView.ShowRowErrors = false;
            flagStatusGridView.Size = new Size(1221, 290);
            flagStatusGridView.TabIndex = 1;
            flagStatusGridView.TabStop = false;
            flagStatusGridView.CellContentClick += flagStatusGridView_CellContentClick;
            // 
            // exploitTestPanel
            // 
            exploitTestPanel.BorderStyle = BorderStyle.FixedSingle;
            exploitTestPanel.Controls.Add(exploitChooseTextBox);
            exploitTestPanel.Controls.Add(runTestButton);
            exploitTestPanel.Controls.Add(teamChooseComboBox);
            exploitTestPanel.Controls.Add(exploitTestPanelLabel);
            exploitTestPanel.Location = new Point(333, 27);
            exploitTestPanel.Name = "exploitTestPanel";
            exploitTestPanel.Size = new Size(375, 125);
            exploitTestPanel.TabIndex = 2;
            exploitTestPanel.Paint += exploitTestPanel_Paint;
            // 
            // exploitChooseTextBox
            // 
            exploitChooseTextBox.BackColor = SystemColors.ButtonHighlight;
            exploitChooseTextBox.BorderStyle = BorderStyle.FixedSingle;
            exploitChooseTextBox.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            exploitChooseTextBox.Location = new Point(3, 68);
            exploitChooseTextBox.Name = "exploitChooseTextBox";
            exploitChooseTextBox.ReadOnly = true;
            exploitChooseTextBox.Size = new Size(208, 27);
            exploitChooseTextBox.TabIndex = 10;
            exploitChooseTextBox.TextChanged += exploitChooseTextBox_TextChanged;
            // 
            // runTestButton
            // 
            runTestButton.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            runTestButton.Location = new Point(217, 35);
            runTestButton.Name = "runTestButton";
            runTestButton.Size = new Size(153, 60);
            runTestButton.TabIndex = 8;
            runTestButton.Text = "START TEST";
            runTestButton.UseVisualStyleBackColor = true;
            runTestButton.Click += runTestButton_Click;
            // 
            // teamChooseComboBox
            // 
            teamChooseComboBox.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            teamChooseComboBox.FormattingEnabled = true;
            teamChooseComboBox.Location = new Point(3, 35);
            teamChooseComboBox.Name = "teamChooseComboBox";
            teamChooseComboBox.Size = new Size(208, 27);
            teamChooseComboBox.TabIndex = 7;
            teamChooseComboBox.SelectedIndexChanged += teamChooseComboBox_SelectedIndexChanged;
            // 
            // exploitTestPanelLabel
            // 
            exploitTestPanelLabel.AutoSize = true;
            exploitTestPanelLabel.BorderStyle = BorderStyle.Fixed3D;
            exploitTestPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            exploitTestPanelLabel.Location = new Point(-1, 0);
            exploitTestPanelLabel.Name = "exploitTestPanelLabel";
            exploitTestPanelLabel.Size = new Size(183, 32);
            exploitTestPanelLabel.TabIndex = 5;
            exploitTestPanelLabel.Text = "EXPLOIT TEST";
            exploitTestPanelLabel.Click += exploitTestPanelLabel_Click;
            // 
            // flagStatusLabel
            // 
            flagStatusLabel.AutoSize = true;
            flagStatusLabel.BorderStyle = BorderStyle.Fixed3D;
            flagStatusLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagStatusLabel.Location = new Point(1053, 0);
            flagStatusLabel.Name = "flagStatusLabel";
            flagStatusLabel.Size = new Size(176, 32);
            flagStatusLabel.TabIndex = 3;
            flagStatusLabel.Text = "FLAG STATUS";
            flagStatusLabel.Click += flagStatusLabel_Click;
            // 
            // manualSubmitPanel
            // 
            manualSubmitPanel.BorderStyle = BorderStyle.FixedSingle;
            manualSubmitPanel.Controls.Add(manualSubmitButton);
            manualSubmitPanel.Controls.Add(manualSubmitTextBox);
            manualSubmitPanel.Controls.Add(manualFlagSubmitPanelLabel);
            manualSubmitPanel.Location = new Point(12, 27);
            manualSubmitPanel.Name = "manualSubmitPanel";
            manualSubmitPanel.Size = new Size(315, 125);
            manualSubmitPanel.TabIndex = 4;
            manualSubmitPanel.Paint += manualSubmitPanel_Paint;
            // 
            // manualSubmitButton
            // 
            manualSubmitButton.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            manualSubmitButton.Location = new Point(3, 70);
            manualSubmitButton.Name = "manualSubmitButton";
            manualSubmitButton.Size = new Size(307, 50);
            manualSubmitButton.TabIndex = 8;
            manualSubmitButton.Text = "SUBMIT";
            manualSubmitButton.UseVisualStyleBackColor = true;
            manualSubmitButton.Click += manualSubmitButton_Click;
            // 
            // manualSubmitTextBox
            // 
            manualSubmitTextBox.Location = new Point(3, 41);
            manualSubmitTextBox.Name = "manualSubmitTextBox";
            manualSubmitTextBox.Size = new Size(307, 23);
            manualSubmitTextBox.TabIndex = 7;
            manualSubmitTextBox.TextChanged += manualSubmitTextBox_TextChanged;
            // 
            // manualFlagSubmitPanelLabel
            // 
            manualFlagSubmitPanelLabel.AutoSize = true;
            manualFlagSubmitPanelLabel.BorderStyle = BorderStyle.Fixed3D;
            manualFlagSubmitPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            manualFlagSubmitPanelLabel.Location = new Point(-1, 0);
            manualFlagSubmitPanelLabel.Name = "manualFlagSubmitPanelLabel";
            manualFlagSubmitPanelLabel.Size = new Size(215, 32);
            manualFlagSubmitPanelLabel.TabIndex = 6;
            manualFlagSubmitPanelLabel.Text = "MANUAL SUBMIT";
            manualFlagSubmitPanelLabel.Click += manualFlagSubmitPanelLabel_Click;
            // 
            // flagTotalAceptedLabel
            // 
            flagTotalAceptedLabel.AutoSize = true;
            flagTotalAceptedLabel.BorderStyle = BorderStyle.Fixed3D;
            flagTotalAceptedLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagTotalAceptedLabel.Location = new Point(-1, 0);
            flagTotalAceptedLabel.Name = "flagTotalAceptedLabel";
            flagTotalAceptedLabel.Size = new Size(333, 32);
            flagTotalAceptedLabel.TabIndex = 5;
            flagTotalAceptedLabel.Text = "TOTAL FLAGS ACCEPTED: 0";
            flagTotalAceptedLabel.Click += flagTotalAceptedLabel_Click;
            // 
            // flagShowFilterPanel
            // 
            flagShowFilterPanel.BorderStyle = BorderStyle.FixedSingle;
            flagShowFilterPanel.Controls.Add(comboBox2);
            flagShowFilterPanel.Controls.Add(comboBox1);
            flagShowFilterPanel.Controls.Add(flagShowFilterPanelLabel);
            flagShowFilterPanel.Location = new Point(12, 286);
            flagShowFilterPanel.Name = "flagShowFilterPanel";
            flagShowFilterPanel.Size = new Size(696, 134);
            flagShowFilterPanel.TabIndex = 6;
            flagShowFilterPanel.Paint += flagShowFilterPanel_Paint;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(3, 64);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(240, 23);
            comboBox2.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 35);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(240, 23);
            comboBox1.TabIndex = 1;
            // 
            // flagShowFilterPanelLabel
            // 
            flagShowFilterPanelLabel.AutoSize = true;
            flagShowFilterPanelLabel.BorderStyle = BorderStyle.Fixed3D;
            flagShowFilterPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagShowFilterPanelLabel.Location = new Point(-1, 0);
            flagShowFilterPanelLabel.Name = "flagShowFilterPanelLabel";
            flagShowFilterPanelLabel.Size = new Size(244, 32);
            flagShowFilterPanelLabel.TabIndex = 0;
            flagShowFilterPanelLabel.Text = "FLAG SHOW FILTER";
            flagShowFilterPanelLabel.Click += flagShowFilterPanelLabel_Click;
            // 
            // teamsPlaceDataGridView
            // 
            teamsPlaceDataGridView.AllowUserToAddRows = false;
            teamsPlaceDataGridView.AllowUserToDeleteRows = false;
            teamsPlaceDataGridView.AllowUserToResizeColumns = false;
            teamsPlaceDataGridView.AllowUserToResizeRows = false;
            teamsPlaceDataGridView.BackgroundColor = SystemColors.Window;
            teamsPlaceDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            teamsPlaceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            teamsPlaceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            teamsPlaceDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            teamsPlaceDataGridView.Location = new Point(3, 35);
            teamsPlaceDataGridView.MultiSelect = false;
            teamsPlaceDataGridView.Name = "teamsPlaceDataGridView";
            teamsPlaceDataGridView.ReadOnly = true;
            teamsPlaceDataGridView.RowTemplate.Height = 25;
            teamsPlaceDataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            teamsPlaceDataGridView.ShowCellErrors = false;
            teamsPlaceDataGridView.ShowCellToolTips = false;
            teamsPlaceDataGridView.ShowEditingIcon = false;
            teamsPlaceDataGridView.ShowRowErrors = false;
            teamsPlaceDataGridView.Size = new Size(520, 328);
            teamsPlaceDataGridView.TabIndex = 7;
            teamsPlaceDataGridView.TabStop = false;
            teamsPlaceDataGridView.CellContentClick += teamsPlaceDataGridView_CellContentClick;
            // 
            // teamsListPanel
            // 
            teamsListPanel.BorderStyle = BorderStyle.FixedSingle;
            teamsListPanel.Controls.Add(AutoTeamsParsFromScoreBoardCheckBox);
            teamsListPanel.Controls.Add(teamsListLabel);
            teamsListPanel.Controls.Add(teamsPlaceDataGridView);
            teamsListPanel.Location = new Point(714, 27);
            teamsListPanel.Name = "teamsListPanel";
            teamsListPanel.Size = new Size(528, 393);
            teamsListPanel.TabIndex = 9;
            teamsListPanel.Paint += teamsListPanel_Paint;
            // 
            // AutoTeamsParsFromScoreBoardCheckBox
            // 
            AutoTeamsParsFromScoreBoardCheckBox.AutoSize = true;
            AutoTeamsParsFromScoreBoardCheckBox.Location = new Point(3, 369);
            AutoTeamsParsFromScoreBoardCheckBox.Name = "AutoTeamsParsFromScoreBoardCheckBox";
            AutoTeamsParsFromScoreBoardCheckBox.Size = new Size(203, 19);
            AutoTeamsParsFromScoreBoardCheckBox.TabIndex = 10;
            AutoTeamsParsFromScoreBoardCheckBox.Text = "Auto teams pars from scoreboard";
            AutoTeamsParsFromScoreBoardCheckBox.UseVisualStyleBackColor = true;
            AutoTeamsParsFromScoreBoardCheckBox.CheckedChanged += AutoTeamsParsFromScoreBoardCheckBox_CheckedChanged;
            // 
            // teamsListLabel
            // 
            teamsListLabel.AutoSize = true;
            teamsListLabel.BorderStyle = BorderStyle.Fixed3D;
            teamsListLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            teamsListLabel.Location = new Point(-1, 0);
            teamsListLabel.Name = "teamsListLabel";
            teamsListLabel.Size = new Size(96, 32);
            teamsListLabel.TabIndex = 6;
            teamsListLabel.Text = "TEAMS";
            teamsListLabel.Click += teamsListLabel_Click;
            // 
            // flagStatusPanel
            // 
            flagStatusPanel.BorderStyle = BorderStyle.FixedSingle;
            flagStatusPanel.Controls.Add(flagTotalAceptedLabel);
            flagStatusPanel.Controls.Add(flagStatusLabel);
            flagStatusPanel.Controls.Add(flagStatusGridView);
            flagStatusPanel.Location = new Point(12, 426);
            flagStatusPanel.Name = "flagStatusPanel";
            flagStatusPanel.Size = new Size(1230, 330);
            flagStatusPanel.TabIndex = 9;
            flagStatusPanel.Paint += flagStatusPanel_Paint;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1256, 768);
            Controls.Add(flagStatusPanel);
            Controls.Add(teamsListPanel);
            Controls.Add(flagShowFilterPanel);
            Controls.Add(manualSubmitPanel);
            Controls.Add(exploitTestPanel);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VQT Farm";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)flagStatusGridView).EndInit();
            exploitTestPanel.ResumeLayout(false);
            exploitTestPanel.PerformLayout();
            manualSubmitPanel.ResumeLayout(false);
            manualSubmitPanel.PerformLayout();
            flagShowFilterPanel.ResumeLayout(false);
            flagShowFilterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)teamsPlaceDataGridView).EndInit();
            teamsListPanel.ResumeLayout(false);
            teamsListPanel.PerformLayout();
            flagStatusPanel.ResumeLayout(false);
            flagStatusPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private DataGridView flagStatusGridView;
        private Panel exploitTestPanel;
        private Label flagStatusLabel;
        private Panel manualSubmitPanel;
        private Button manualSubmitButton;
        private TextBox manualSubmitTextBox;
        private Label manualFlagSubmitPanelLabel;
        private Label flagTotalAceptedLabel;
        private Label exploitTestPanelLabel;
        private Button runTestButton;
        private Panel flagShowFilterPanel;
        private Label flagShowFilterPanelLabel;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private DataGridView teamsPlaceDataGridView;
        private Panel teamsListPanel;
        private Label teamsListLabel;
        private Panel flagStatusPanel;
        private ToolStripMenuItem addTeamManualToolStripMenuItem;
        private CheckBox AutoTeamsParsFromScoreBoardCheckBox;
        private ComboBox teamChooseComboBox;
        private TextBox exploitChooseTextBox;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem teamsToolStripMenuItem;
        private ToolStripMenuItem flagHistoryToolStripMenuItem;
        private ToolStripMenuItem manualSubmitToolStripMenuItem;
        private ToolStripMenuItem exploitTestToolStripMenuItem;
        private ToolStripMenuItem flagShowFilterToolStripMenuItem;
    }
}