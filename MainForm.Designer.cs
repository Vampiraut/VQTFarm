﻿namespace VQTFarm
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            addTeamManualToolStripMenuItem = new ToolStripMenuItem();
            autoTeamsGenerationToolStripMenuItem = new ToolStripMenuItem();
            startStopFarmToolStripMenuItem = new ToolStripMenuItem();
            onOffAutoTeamParseToolStripMenuItem = new ToolStripMenuItem();
            onOffConectionCheckToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            teamsToolStripMenuItem = new ToolStripMenuItem();
            flagHistoryToolStripMenuItem = new ToolStripMenuItem();
            manualSubmitToolStripMenuItem = new ToolStripMenuItem();
            exploitTestToolStripMenuItem = new ToolStripMenuItem();
            flagShowFilterToolStripMenuItem = new ToolStripMenuItem();
            DisablePopUpMessagesToolStripMenuItem = new ToolStripMenuItem();
            informationToolStripMenuItem = new ToolStripMenuItem();
            warningsToolStripMenuItem = new ToolStripMenuItem();
            errorsToolStripMenuItem = new ToolStripMenuItem();
            fixTablesToolStripMenuItem = new ToolStripMenuItem();
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
            flagTotalSendedLabel = new Label();
            flagShowFilterPanel = new Panel();
            ClearFiltersInputCheckBox = new CheckBox();
            applyFilterButton = new Button();
            cheskSystemResponsFilterTextBox = new TextBox();
            statusFilterTextBox = new TextBox();
            flagFilterTextBox = new TextBox();
            teamFilterTextBox = new TextBox();
            exploitFilterTextBox = new TextBox();
            flagShowFilterPanelLabel = new Label();
            teamsPlaceDataGridView = new DataGridView();
            teamsListPanel = new Panel();
            pagesOfMaxForTeamsPanelLabel = new Label();
            curPageTeamsTableTextBox = new TextBox();
            pageForTeamsPanelLabel = new Label();
            nextPageTeamsTableButton = new Button();
            prevPageTeamsTableButton = new Button();
            teamsListLabel = new Label();
            flagStatusPanel = new Panel();
            pagesOfMaxForFlagsPanelLabel = new Label();
            curPageFlagsTableTextBox = new TextBox();
            pageForFlagsPanelLabel = new Label();
            nextPageFlagsTableButton = new Button();
            prevPageFlagsTableButton = new Button();
            flagTotalAceptedLabel = new Label();
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
            menuStrip1.BackColor = Color.Transparent;
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
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addTeamManualToolStripMenuItem, autoTeamsGenerationToolStripMenuItem, startStopFarmToolStripMenuItem, onOffAutoTeamParseToolStripMenuItem, onOffConectionCheckToolStripMenuItem, showToolStripMenuItem, DisablePopUpMessagesToolStripMenuItem, fixTablesToolStripMenuItem });
            settingsToolStripMenuItem.ForeColor = Color.Lime;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // addTeamManualToolStripMenuItem
            // 
            addTeamManualToolStripMenuItem.ForeColor = SystemColors.ControlText;
            addTeamManualToolStripMenuItem.Name = "addTeamManualToolStripMenuItem";
            addTeamManualToolStripMenuItem.Size = new Size(214, 22);
            addTeamManualToolStripMenuItem.Text = "Add team manual";
            addTeamManualToolStripMenuItem.Click += addTeamManualToolStripMenuItem_Click;
            // 
            // autoTeamsGenerationToolStripMenuItem
            // 
            autoTeamsGenerationToolStripMenuItem.Name = "autoTeamsGenerationToolStripMenuItem";
            autoTeamsGenerationToolStripMenuItem.Size = new Size(214, 22);
            autoTeamsGenerationToolStripMenuItem.Text = "Auto teams generation";
            autoTeamsGenerationToolStripMenuItem.Click += autoTeamsGenerationToolStripMenuItem_Click;
            // 
            // startStopFarmToolStripMenuItem
            // 
            startStopFarmToolStripMenuItem.Name = "startStopFarmToolStripMenuItem";
            startStopFarmToolStripMenuItem.Size = new Size(214, 22);
            startStopFarmToolStripMenuItem.Text = "Start/Stop Farm";
            startStopFarmToolStripMenuItem.Click += startStopFarmToolStripMenuItem_Click;
            // 
            // onOffAutoTeamParseToolStripMenuItem
            // 
            onOffAutoTeamParseToolStripMenuItem.ForeColor = Color.Red;
            onOffAutoTeamParseToolStripMenuItem.Name = "onOffAutoTeamParseToolStripMenuItem";
            onOffAutoTeamParseToolStripMenuItem.Size = new Size(214, 22);
            onOffAutoTeamParseToolStripMenuItem.Text = "On Auto team parse (OFF)";
            onOffAutoTeamParseToolStripMenuItem.Click += onOffAutoTeamParseToolStripMenuItem_Click;
            // 
            // onOffConectionCheckToolStripMenuItem
            // 
            onOffConectionCheckToolStripMenuItem.ForeColor = Color.Red;
            onOffConectionCheckToolStripMenuItem.Name = "onOffConectionCheckToolStripMenuItem";
            onOffConectionCheckToolStripMenuItem.Size = new Size(214, 22);
            onOffConectionCheckToolStripMenuItem.Text = "On Conection check (OFF)";
            onOffConectionCheckToolStripMenuItem.Click += onOffConectionCheckToolStripMenuItem_Click;
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { teamsToolStripMenuItem, flagHistoryToolStripMenuItem, manualSubmitToolStripMenuItem, exploitTestToolStripMenuItem, flagShowFilterToolStripMenuItem });
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(214, 22);
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
            // DisablePopUpMessagesToolStripMenuItem
            // 
            DisablePopUpMessagesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { informationToolStripMenuItem, warningsToolStripMenuItem, errorsToolStripMenuItem });
            DisablePopUpMessagesToolStripMenuItem.Name = "DisablePopUpMessagesToolStripMenuItem";
            DisablePopUpMessagesToolStripMenuItem.Size = new Size(214, 22);
            DisablePopUpMessagesToolStripMenuItem.Text = "Disable pop-up messages";
            DisablePopUpMessagesToolStripMenuItem.Click += disablePopUpMessagesToolStripMenuItem_Click;
            // 
            // informationToolStripMenuItem
            // 
            informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            informationToolStripMenuItem.Size = new Size(137, 22);
            informationToolStripMenuItem.Text = "Information";
            informationToolStripMenuItem.Click += informationToolStripMenuItem_Click;
            // 
            // warningsToolStripMenuItem
            // 
            warningsToolStripMenuItem.Name = "warningsToolStripMenuItem";
            warningsToolStripMenuItem.Size = new Size(137, 22);
            warningsToolStripMenuItem.Text = "Warnings";
            warningsToolStripMenuItem.Click += warningsToolStripMenuItem_Click;
            // 
            // errorsToolStripMenuItem
            // 
            errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            errorsToolStripMenuItem.Size = new Size(137, 22);
            errorsToolStripMenuItem.Text = "Errors";
            errorsToolStripMenuItem.Click += errorsToolStripMenuItem_Click;
            // 
            // fixTablesToolStripMenuItem
            // 
            fixTablesToolStripMenuItem.Name = "fixTablesToolStripMenuItem";
            fixTablesToolStripMenuItem.Size = new Size(214, 22);
            fixTablesToolStripMenuItem.Text = "Fix Tables";
            fixTablesToolStripMenuItem.Click += fixTablesToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.BackColor = Color.Transparent;
            helpToolStripMenuItem.ForeColor = Color.Lime;
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
            flagStatusGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            flagStatusGridView.BackgroundColor = Color.DarkBlue;
            flagStatusGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            flagStatusGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            flagStatusGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.HighlightText;
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
            flagStatusGridView.Size = new Size(1222, 372);
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
            exploitTestPanel.Size = new Size(375, 116);
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
            runTestButton.BackColor = SystemColors.WindowText;
            runTestButton.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            runTestButton.ForeColor = Color.Lime;
            runTestButton.Location = new Point(217, 35);
            runTestButton.Name = "runTestButton";
            runTestButton.Size = new Size(153, 60);
            runTestButton.TabIndex = 8;
            runTestButton.Text = "START TEST";
            runTestButton.UseVisualStyleBackColor = false;
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
            exploitTestPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            exploitTestPanelLabel.ForeColor = Color.Lime;
            exploitTestPanelLabel.Location = new Point(-1, 0);
            exploitTestPanelLabel.Name = "exploitTestPanelLabel";
            exploitTestPanelLabel.Size = new Size(165, 30);
            exploitTestPanelLabel.TabIndex = 5;
            exploitTestPanelLabel.Text = "SPLOIT TEST";
            exploitTestPanelLabel.Click += exploitTestPanelLabel_Click;
            // 
            // flagStatusLabel
            // 
            flagStatusLabel.AutoSize = true;
            flagStatusLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagStatusLabel.ForeColor = Color.Lime;
            flagStatusLabel.Location = new Point(519, 0);
            flagStatusLabel.Name = "flagStatusLabel";
            flagStatusLabel.Size = new Size(174, 30);
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
            manualSubmitPanel.Size = new Size(315, 116);
            manualSubmitPanel.TabIndex = 4;
            manualSubmitPanel.Paint += manualSubmitPanel_Paint;
            // 
            // manualSubmitButton
            // 
            manualSubmitButton.BackColor = SystemColors.WindowText;
            manualSubmitButton.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            manualSubmitButton.ForeColor = Color.Lime;
            manualSubmitButton.Location = new Point(3, 64);
            manualSubmitButton.Name = "manualSubmitButton";
            manualSubmitButton.Size = new Size(307, 47);
            manualSubmitButton.TabIndex = 8;
            manualSubmitButton.Text = "SUBMIT";
            manualSubmitButton.UseVisualStyleBackColor = false;
            manualSubmitButton.Click += manualSubmitButton_Click;
            // 
            // manualSubmitTextBox
            // 
            manualSubmitTextBox.Location = new Point(3, 35);
            manualSubmitTextBox.Name = "manualSubmitTextBox";
            manualSubmitTextBox.Size = new Size(307, 23);
            manualSubmitTextBox.TabIndex = 7;
            manualSubmitTextBox.TextChanged += manualSubmitTextBox_TextChanged;
            // 
            // manualFlagSubmitPanelLabel
            // 
            manualFlagSubmitPanelLabel.AutoSize = true;
            manualFlagSubmitPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            manualFlagSubmitPanelLabel.ForeColor = Color.Lime;
            manualFlagSubmitPanelLabel.Location = new Point(-1, 0);
            manualFlagSubmitPanelLabel.Name = "manualFlagSubmitPanelLabel";
            manualFlagSubmitPanelLabel.Size = new Size(213, 30);
            manualFlagSubmitPanelLabel.TabIndex = 6;
            manualFlagSubmitPanelLabel.Text = "MANUAL SUBMIT";
            manualFlagSubmitPanelLabel.Click += manualFlagSubmitPanelLabel_Click;
            // 
            // flagTotalSendedLabel
            // 
            flagTotalSendedLabel.AutoSize = true;
            flagTotalSendedLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagTotalSendedLabel.ForeColor = Color.Lime;
            flagTotalSendedLabel.Location = new Point(-1, 0);
            flagTotalSendedLabel.Name = "flagTotalSendedLabel";
            flagTotalSendedLabel.Size = new Size(302, 30);
            flagTotalSendedLabel.TabIndex = 5;
            flagTotalSendedLabel.Text = "TOTAL FLAGS SENDED: 0";
            flagTotalSendedLabel.Click += flagTotalSendedLabel_Click;
            // 
            // flagShowFilterPanel
            // 
            flagShowFilterPanel.BorderStyle = BorderStyle.FixedSingle;
            flagShowFilterPanel.Controls.Add(ClearFiltersInputCheckBox);
            flagShowFilterPanel.Controls.Add(applyFilterButton);
            flagShowFilterPanel.Controls.Add(cheskSystemResponsFilterTextBox);
            flagShowFilterPanel.Controls.Add(statusFilterTextBox);
            flagShowFilterPanel.Controls.Add(flagFilterTextBox);
            flagShowFilterPanel.Controls.Add(teamFilterTextBox);
            flagShowFilterPanel.Controls.Add(exploitFilterTextBox);
            flagShowFilterPanel.Controls.Add(flagShowFilterPanelLabel);
            flagShowFilterPanel.Location = new Point(12, 286);
            flagShowFilterPanel.Name = "flagShowFilterPanel";
            flagShowFilterPanel.Size = new Size(696, 134);
            flagShowFilterPanel.TabIndex = 6;
            flagShowFilterPanel.Paint += flagShowFilterPanel_Paint;
            // 
            // ClearFiltersInputCheckBox
            // 
            ClearFiltersInputCheckBox.AutoSize = true;
            ClearFiltersInputCheckBox.ForeColor = Color.Lime;
            ClearFiltersInputCheckBox.Location = new Point(464, 110);
            ClearFiltersInputCheckBox.Name = "ClearFiltersInputCheckBox";
            ClearFiltersInputCheckBox.Size = new Size(227, 19);
            ClearFiltersInputCheckBox.TabIndex = 12;
            ClearFiltersInputCheckBox.Text = "Clean the entered filters after applying";
            ClearFiltersInputCheckBox.UseVisualStyleBackColor = true;
            ClearFiltersInputCheckBox.CheckedChanged += ClearFiltersInputCheckBox_CheckedChanged;
            // 
            // applyFilterButton
            // 
            applyFilterButton.BackColor = SystemColors.WindowText;
            applyFilterButton.Font = new Font("Arial Black", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            applyFilterButton.ForeColor = Color.Lime;
            applyFilterButton.Location = new Point(511, 61);
            applyFilterButton.Name = "applyFilterButton";
            applyFilterButton.Size = new Size(180, 43);
            applyFilterButton.TabIndex = 11;
            applyFilterButton.Text = "APPLY FILTER";
            applyFilterButton.UseVisualStyleBackColor = false;
            applyFilterButton.Click += applyFilterButton_Click;
            // 
            // cheskSystemResponsFilterTextBox
            // 
            cheskSystemResponsFilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            cheskSystemResponsFilterTextBox.Location = new Point(3, 93);
            cheskSystemResponsFilterTextBox.Name = "cheskSystemResponsFilterTextBox";
            cheskSystemResponsFilterTextBox.Size = new Size(412, 23);
            cheskSystemResponsFilterTextBox.TabIndex = 8;
            cheskSystemResponsFilterTextBox.TextChanged += cheskSystemResponsFilterTextBox_TextChanged;
            // 
            // statusFilterTextBox
            // 
            statusFilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            statusFilterTextBox.Location = new Point(274, 64);
            statusFilterTextBox.Name = "statusFilterTextBox";
            statusFilterTextBox.Size = new Size(141, 23);
            statusFilterTextBox.TabIndex = 7;
            statusFilterTextBox.TextChanged += statusFilterTextBox_TextChanged;
            // 
            // flagFilterTextBox
            // 
            flagFilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            flagFilterTextBox.Location = new Point(3, 64);
            flagFilterTextBox.Name = "flagFilterTextBox";
            flagFilterTextBox.Size = new Size(265, 23);
            flagFilterTextBox.TabIndex = 6;
            flagFilterTextBox.TextChanged += flagFilterTextBox_TextChanged;
            // 
            // teamFilterTextBox
            // 
            teamFilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            teamFilterTextBox.Location = new Point(212, 35);
            teamFilterTextBox.Name = "teamFilterTextBox";
            teamFilterTextBox.Size = new Size(203, 23);
            teamFilterTextBox.TabIndex = 3;
            teamFilterTextBox.TextChanged += teamFilterTextBox_TextChanged;
            // 
            // exploitFilterTextBox
            // 
            exploitFilterTextBox.BorderStyle = BorderStyle.FixedSingle;
            exploitFilterTextBox.Location = new Point(3, 35);
            exploitFilterTextBox.Name = "exploitFilterTextBox";
            exploitFilterTextBox.Size = new Size(203, 23);
            exploitFilterTextBox.TabIndex = 1;
            exploitFilterTextBox.TextChanged += exploitFilterTextBox_TextChanged;
            // 
            // flagShowFilterPanelLabel
            // 
            flagShowFilterPanelLabel.AutoSize = true;
            flagShowFilterPanelLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagShowFilterPanelLabel.ForeColor = Color.Lime;
            flagShowFilterPanelLabel.Location = new Point(-1, 0);
            flagShowFilterPanelLabel.Name = "flagShowFilterPanelLabel";
            flagShowFilterPanelLabel.Size = new Size(242, 30);
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
            teamsPlaceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            teamsPlaceDataGridView.BackgroundColor = Color.DarkBlue;
            teamsPlaceDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            teamsPlaceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            teamsPlaceDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            teamsPlaceDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            teamsPlaceDataGridView.Location = new Point(3, 35);
            teamsPlaceDataGridView.MultiSelect = false;
            teamsPlaceDataGridView.Name = "teamsPlaceDataGridView";
            teamsPlaceDataGridView.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            teamsPlaceDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
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
            teamsListPanel.Controls.Add(pagesOfMaxForTeamsPanelLabel);
            teamsListPanel.Controls.Add(curPageTeamsTableTextBox);
            teamsListPanel.Controls.Add(pageForTeamsPanelLabel);
            teamsListPanel.Controls.Add(nextPageTeamsTableButton);
            teamsListPanel.Controls.Add(prevPageTeamsTableButton);
            teamsListPanel.Controls.Add(teamsListLabel);
            teamsListPanel.Controls.Add(teamsPlaceDataGridView);
            teamsListPanel.Location = new Point(714, 27);
            teamsListPanel.Name = "teamsListPanel";
            teamsListPanel.Size = new Size(528, 393);
            teamsListPanel.TabIndex = 9;
            teamsListPanel.Paint += teamsListPanel_Paint;
            // 
            // pagesOfMaxForTeamsPanelLabel
            // 
            pagesOfMaxForTeamsPanelLabel.AutoSize = true;
            pagesOfMaxForTeamsPanelLabel.ForeColor = Color.Lime;
            pagesOfMaxForTeamsPanelLabel.Location = new Point(413, 10);
            pagesOfMaxForTeamsPanelLabel.Name = "pagesOfMaxForTeamsPanelLabel";
            pagesOfMaxForTeamsPanelLabel.Size = new Size(75, 15);
            pagesOfMaxForTeamsPanelLabel.TabIndex = 18;
            pagesOfMaxForTeamsPanelLabel.Text = "of 999999999";
            pagesOfMaxForTeamsPanelLabel.Click += pagesOfMaxForTeamsPanelLabel_Click;
            // 
            // curPageTeamsTableTextBox
            // 
            curPageTeamsTableTextBox.BackColor = Color.DarkBlue;
            curPageTeamsTableTextBox.BorderStyle = BorderStyle.None;
            curPageTeamsTableTextBox.ForeColor = Color.Lime;
            curPageTeamsTableTextBox.Location = new Point(346, 10);
            curPageTeamsTableTextBox.Name = "curPageTeamsTableTextBox";
            curPageTeamsTableTextBox.ReadOnly = true;
            curPageTeamsTableTextBox.Size = new Size(61, 16);
            curPageTeamsTableTextBox.TabIndex = 17;
            curPageTeamsTableTextBox.Text = "999999999";
            curPageTeamsTableTextBox.TextChanged += curPageTeamsTableTextBox_TextChanged;
            // 
            // pageForTeamsPanelLabel
            // 
            pageForTeamsPanelLabel.AutoSize = true;
            pageForTeamsPanelLabel.ForeColor = Color.Lime;
            pageForTeamsPanelLabel.Location = new Point(304, 10);
            pageForTeamsPanelLabel.Name = "pageForTeamsPanelLabel";
            pageForTeamsPanelLabel.Size = new Size(36, 15);
            pageForTeamsPanelLabel.TabIndex = 14;
            pageForTeamsPanelLabel.Text = "Page:";
            pageForTeamsPanelLabel.Click += pageForTeamsPanelLabel_Click;
            // 
            // nextPageTeamsTableButton
            // 
            nextPageTeamsTableButton.BackColor = Color.Black;
            nextPageTeamsTableButton.ForeColor = Color.Lime;
            nextPageTeamsTableButton.Location = new Point(494, 3);
            nextPageTeamsTableButton.Name = "nextPageTeamsTableButton";
            nextPageTeamsTableButton.Size = new Size(29, 29);
            nextPageTeamsTableButton.TabIndex = 13;
            nextPageTeamsTableButton.Text = ">";
            nextPageTeamsTableButton.UseVisualStyleBackColor = false;
            nextPageTeamsTableButton.Click += nextPageTeamsTableButton_Click;
            // 
            // prevPageTeamsTableButton
            // 
            prevPageTeamsTableButton.BackColor = Color.Black;
            prevPageTeamsTableButton.ForeColor = Color.Lime;
            prevPageTeamsTableButton.Location = new Point(269, 3);
            prevPageTeamsTableButton.Name = "prevPageTeamsTableButton";
            prevPageTeamsTableButton.Size = new Size(29, 29);
            prevPageTeamsTableButton.TabIndex = 12;
            prevPageTeamsTableButton.Text = "<";
            prevPageTeamsTableButton.UseVisualStyleBackColor = false;
            prevPageTeamsTableButton.Click += prevPageTeamsTableButton_Click;
            // 
            // teamsListLabel
            // 
            teamsListLabel.AutoSize = true;
            teamsListLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            teamsListLabel.ForeColor = Color.Lime;
            teamsListLabel.Location = new Point(-1, 0);
            teamsListLabel.Name = "teamsListLabel";
            teamsListLabel.Size = new Size(94, 30);
            teamsListLabel.TabIndex = 6;
            teamsListLabel.Text = "TEAMS";
            teamsListLabel.Click += teamsListLabel_Click;
            // 
            // flagStatusPanel
            // 
            flagStatusPanel.BorderStyle = BorderStyle.FixedSingle;
            flagStatusPanel.Controls.Add(pagesOfMaxForFlagsPanelLabel);
            flagStatusPanel.Controls.Add(curPageFlagsTableTextBox);
            flagStatusPanel.Controls.Add(pageForFlagsPanelLabel);
            flagStatusPanel.Controls.Add(nextPageFlagsTableButton);
            flagStatusPanel.Controls.Add(prevPageFlagsTableButton);
            flagStatusPanel.Controls.Add(flagTotalAceptedLabel);
            flagStatusPanel.Controls.Add(flagTotalSendedLabel);
            flagStatusPanel.Controls.Add(flagStatusLabel);
            flagStatusPanel.Controls.Add(flagStatusGridView);
            flagStatusPanel.Location = new Point(12, 426);
            flagStatusPanel.Name = "flagStatusPanel";
            flagStatusPanel.Size = new Size(1230, 443);
            flagStatusPanel.TabIndex = 9;
            flagStatusPanel.Paint += flagStatusPanel_Paint;
            // 
            // pagesOfMaxForFlagsPanelLabel
            // 
            pagesOfMaxForFlagsPanelLabel.AutoSize = true;
            pagesOfMaxForFlagsPanelLabel.ForeColor = Color.Lime;
            pagesOfMaxForFlagsPanelLabel.Location = new Point(1115, 416);
            pagesOfMaxForFlagsPanelLabel.Name = "pagesOfMaxForFlagsPanelLabel";
            pagesOfMaxForFlagsPanelLabel.Size = new Size(75, 15);
            pagesOfMaxForFlagsPanelLabel.TabIndex = 17;
            pagesOfMaxForFlagsPanelLabel.Text = "of 999999999";
            pagesOfMaxForFlagsPanelLabel.Click += pagesOfMaxForFlagsPanelLabel_Click;
            // 
            // curPageFlagsTableTextBox
            // 
            curPageFlagsTableTextBox.BackColor = Color.DarkBlue;
            curPageFlagsTableTextBox.BorderStyle = BorderStyle.None;
            curPageFlagsTableTextBox.ForeColor = Color.Lime;
            curPageFlagsTableTextBox.Location = new Point(1048, 416);
            curPageFlagsTableTextBox.Name = "curPageFlagsTableTextBox";
            curPageFlagsTableTextBox.ReadOnly = true;
            curPageFlagsTableTextBox.Size = new Size(61, 16);
            curPageFlagsTableTextBox.TabIndex = 16;
            curPageFlagsTableTextBox.Text = "999999999";
            curPageFlagsTableTextBox.TextChanged += curPageFlagsTableTextBox_TextChanged;
            // 
            // pageForFlagsPanelLabel
            // 
            pageForFlagsPanelLabel.AutoSize = true;
            pageForFlagsPanelLabel.ForeColor = Color.Lime;
            pageForFlagsPanelLabel.Location = new Point(1006, 416);
            pageForFlagsPanelLabel.Name = "pageForFlagsPanelLabel";
            pageForFlagsPanelLabel.Size = new Size(36, 15);
            pageForFlagsPanelLabel.TabIndex = 15;
            pageForFlagsPanelLabel.Text = "Page:";
            pageForFlagsPanelLabel.Click += pageForFlagsPanelLabel_Click;
            // 
            // nextPageFlagsTableButton
            // 
            nextPageFlagsTableButton.BackColor = Color.Black;
            nextPageFlagsTableButton.ForeColor = Color.Lime;
            nextPageFlagsTableButton.Location = new Point(1196, 409);
            nextPageFlagsTableButton.Name = "nextPageFlagsTableButton";
            nextPageFlagsTableButton.Size = new Size(29, 29);
            nextPageFlagsTableButton.TabIndex = 14;
            nextPageFlagsTableButton.Text = ">";
            nextPageFlagsTableButton.UseVisualStyleBackColor = false;
            nextPageFlagsTableButton.Click += nextPageFlagsTableButton_Click;
            // 
            // prevPageFlagsTableButton
            // 
            prevPageFlagsTableButton.BackColor = Color.Black;
            prevPageFlagsTableButton.ForeColor = Color.Lime;
            prevPageFlagsTableButton.Location = new Point(971, 409);
            prevPageFlagsTableButton.Name = "prevPageFlagsTableButton";
            prevPageFlagsTableButton.Size = new Size(29, 29);
            prevPageFlagsTableButton.TabIndex = 13;
            prevPageFlagsTableButton.Text = "<";
            prevPageFlagsTableButton.UseVisualStyleBackColor = false;
            prevPageFlagsTableButton.Click += prevPageFlagsTableButton_Click;
            // 
            // flagTotalAceptedLabel
            // 
            flagTotalAceptedLabel.AutoSize = true;
            flagTotalAceptedLabel.Font = new Font("Arial Black", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            flagTotalAceptedLabel.ForeColor = Color.Lime;
            flagTotalAceptedLabel.Location = new Point(821, 0);
            flagTotalAceptedLabel.Name = "flagTotalAceptedLabel";
            flagTotalAceptedLabel.Size = new Size(331, 30);
            flagTotalAceptedLabel.TabIndex = 6;
            flagTotalAceptedLabel.Text = "TOTAL FLAGS ACCEPTED: 0";
            flagTotalAceptedLabel.Click += flagTotalAceptedLabel_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkBlue;
            ClientSize = new Size(1256, 881);
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
        private Label flagTotalSendedLabel;
        private Label exploitTestPanelLabel;
        private Button runTestButton;
        private Panel flagShowFilterPanel;
        private Label flagShowFilterPanelLabel;
        private DataGridView teamsPlaceDataGridView;
        private Panel teamsListPanel;
        private Label teamsListLabel;
        private Panel flagStatusPanel;
        private ToolStripMenuItem addTeamManualToolStripMenuItem;
        private ComboBox teamChooseComboBox;
        private TextBox exploitChooseTextBox;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem teamsToolStripMenuItem;
        private ToolStripMenuItem flagHistoryToolStripMenuItem;
        private ToolStripMenuItem manualSubmitToolStripMenuItem;
        private ToolStripMenuItem exploitTestToolStripMenuItem;
        private ToolStripMenuItem flagShowFilterToolStripMenuItem;
        private TextBox exploitFilterTextBox;
        private TextBox teamFilterTextBox;
        private Label flagTotalAceptedLabel;
        private TextBox flagFilterTextBox;
        private TextBox statusFilterTextBox;
        private TextBox cheskSystemResponsFilterTextBox;
        private Button applyFilterButton;
        private CheckBox ClearFiltersInputCheckBox;
        private ToolStripMenuItem startStopFarmToolStripMenuItem;
        private ToolStripMenuItem fixTablesToolStripMenuItem;
        private Label pageForTeamsPanelLabel;
        private Button nextPageTeamsTableButton;
        private Button prevPageTeamsTableButton;
        private Label pagesOfMaxForFlagsPanelLabel;
        private TextBox curPageFlagsTableTextBox;
        private Label pageForFlagsPanelLabel;
        private Button nextPageFlagsTableButton;
        private Button prevPageFlagsTableButton;
        private TextBox curPageTeamsTableTextBox;
        private Label pagesOfMaxForTeamsPanelLabel;
        private ToolStripMenuItem onOffConectionCheckToolStripMenuItem;
        private ToolStripMenuItem onOffAutoTeamParseToolStripMenuItem;
        private ToolStripMenuItem DisablePopUpMessagesToolStripMenuItem;
        private ToolStripMenuItem informationToolStripMenuItem;
        private ToolStripMenuItem warningsToolStripMenuItem;
        private ToolStripMenuItem errorsToolStripMenuItem;
        private ToolStripMenuItem autoTeamsGenerationToolStripMenuItem;
    }
}