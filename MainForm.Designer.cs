





namespace TradingAssistant
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
            mainTabControl = new TabControl();
            coinListTabPage = new TabPage();
            splitContainer1 = new SplitContainer();
            allCoinsTableLayoutPanel = new TableLayoutPanel();
            addCoinsToTrackedButton = new Button();
            allCoinsDataGridView = new Zuby.ADGV.AdvancedDataGridView();
            trackedCoinsTableLayoutPanel = new TableLayoutPanel();
            removeTrackedCoinsButton = new Button();
            trackedCoinsDataGridView = new Zuby.ADGV.AdvancedDataGridView();
            settingsTabPage = new TabPage();
            settingsTabControl = new TabControl();
            apiSettingsTabPage = new TabPage();
            apiSettingsTabControl = new TabControl();
            coinDeskSettingsTabPage = new TabPage();
            coinDeskSettingsTableLayoutPanel = new TableLayoutPanel();
            coinDeskApiKeyTableLayoutPanel = new TableLayoutPanel();
            label5 = new Label();
            coinDeskApiKeyTextBox = new TextBox();
            polygonSettingsTabPage = new TabPage();
            polygonSettingsTableLayoutPanel = new TableLayoutPanel();
            polygonApiKeyTableLayoutPanel = new TableLayoutPanel();
            label1 = new Label();
            polygonApiKeyTextBox = new TextBox();
            polygonRateLimitGroupBox = new GroupBox();
            polygonQueriesPerMinuteFlowLayoutPanel = new FlowLayoutPanel();
            polygonLimitQueriesPerMinuteCheckbox = new CheckBox();
            polygonQueriesPerMinuteNumericUpDown = new NumericUpDown();
            label2 = new Label();
            polygonIgnoreInactiveTickersCheckbox = new CheckBox();
            polygonRefreshSettingsGroupBox = new GroupBox();
            tickerListRefreshFlowLayoutPanel = new FlowLayoutPanel();
            label3 = new Label();
            tickerListRefreshIntervalDaysUpDown = new NumericUpDown();
            label4 = new Label();
            refreshTickerListButton = new Button();
            tickerListLastRefreshedLabel = new Label();
            chatGPTSettingsTabPage = new TabPage();
            tabPage3 = new TabPage();
            mainTabControl.SuspendLayout();
            coinListTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            allCoinsTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)allCoinsDataGridView).BeginInit();
            trackedCoinsTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackedCoinsDataGridView).BeginInit();
            settingsTabPage.SuspendLayout();
            settingsTabControl.SuspendLayout();
            apiSettingsTabPage.SuspendLayout();
            apiSettingsTabControl.SuspendLayout();
            coinDeskSettingsTabPage.SuspendLayout();
            coinDeskSettingsTableLayoutPanel.SuspendLayout();
            coinDeskApiKeyTableLayoutPanel.SuspendLayout();
            polygonSettingsTabPage.SuspendLayout();
            polygonSettingsTableLayoutPanel.SuspendLayout();
            polygonApiKeyTableLayoutPanel.SuspendLayout();
            polygonRateLimitGroupBox.SuspendLayout();
            polygonQueriesPerMinuteFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)polygonQueriesPerMinuteNumericUpDown).BeginInit();
            polygonRefreshSettingsGroupBox.SuspendLayout();
            tickerListRefreshFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tickerListRefreshIntervalDaysUpDown).BeginInit();
            SuspendLayout();
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(coinListTabPage);
            mainTabControl.Controls.Add(settingsTabPage);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(0, 0);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(800, 450);
            mainTabControl.TabIndex = 0;
            // 
            // coinListTabPage
            // 
            coinListTabPage.Controls.Add(splitContainer1);
            coinListTabPage.Location = new Point(4, 24);
            coinListTabPage.Name = "coinListTabPage";
            coinListTabPage.Padding = new Padding(3);
            coinListTabPage.Size = new Size(792, 422);
            coinListTabPage.TabIndex = 0;
            coinListTabPage.Text = "Coin List";
            coinListTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(allCoinsTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(trackedCoinsTableLayoutPanel);
            splitContainer1.Size = new Size(786, 416);
            splitContainer1.SplitterDistance = 262;
            splitContainer1.TabIndex = 0;
            // 
            // allCoinsTableLayoutPanel
            // 
            allCoinsTableLayoutPanel.ColumnCount = 1;
            allCoinsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            allCoinsTableLayoutPanel.Controls.Add(addCoinsToTrackedButton, 0, 1);
            allCoinsTableLayoutPanel.Controls.Add(allCoinsDataGridView, 0, 0);
            allCoinsTableLayoutPanel.Dock = DockStyle.Fill;
            allCoinsTableLayoutPanel.Location = new Point(0, 0);
            allCoinsTableLayoutPanel.Name = "allCoinsTableLayoutPanel";
            allCoinsTableLayoutPanel.RowCount = 2;
            allCoinsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            allCoinsTableLayoutPanel.RowStyles.Add(new RowStyle());
            allCoinsTableLayoutPanel.Size = new Size(786, 262);
            allCoinsTableLayoutPanel.TabIndex = 0;
            // 
            // addCoinsToTrackedButton
            // 
            addCoinsToTrackedButton.Dock = DockStyle.Bottom;
            addCoinsToTrackedButton.Location = new Point(3, 236);
            addCoinsToTrackedButton.Name = "addCoinsToTrackedButton";
            addCoinsToTrackedButton.Size = new Size(780, 23);
            addCoinsToTrackedButton.TabIndex = 0;
            addCoinsToTrackedButton.Text = "Add Selected Coins to Tracked Coins List";
            addCoinsToTrackedButton.UseVisualStyleBackColor = true;
            addCoinsToTrackedButton.Click += addCoinsToTrackedButton_Click;
            // 
            // allCoinsDataGridView
            // 
            allCoinsDataGridView.AllowUserToAddRows = false;
            allCoinsDataGridView.AllowUserToDeleteRows = false;
            allCoinsDataGridView.AllowUserToOrderColumns = true;
            allCoinsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            allCoinsDataGridView.Dock = DockStyle.Fill;
            allCoinsDataGridView.FilterAndSortEnabled = true;
            allCoinsDataGridView.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            allCoinsDataGridView.Location = new Point(3, 3);
            allCoinsDataGridView.MaxFilterButtonImageHeight = 23;
            allCoinsDataGridView.Name = "allCoinsDataGridView";
            allCoinsDataGridView.ReadOnly = true;
            allCoinsDataGridView.RightToLeft = RightToLeft.No;
            allCoinsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            allCoinsDataGridView.Size = new Size(780, 227);
            allCoinsDataGridView.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            allCoinsDataGridView.TabIndex = 1;
            allCoinsDataGridView.ColumnAdded += DataGridViewColumnAdded;
            allCoinsDataGridView.DataBindingComplete += allCoinsDataGridView_DataBindingComplete;
            // 
            // trackedCoinsTableLayoutPanel
            // 
            trackedCoinsTableLayoutPanel.ColumnCount = 1;
            trackedCoinsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            trackedCoinsTableLayoutPanel.Controls.Add(removeTrackedCoinsButton, 0, 1);
            trackedCoinsTableLayoutPanel.Controls.Add(trackedCoinsDataGridView, 0, 0);
            trackedCoinsTableLayoutPanel.Dock = DockStyle.Fill;
            trackedCoinsTableLayoutPanel.Location = new Point(0, 0);
            trackedCoinsTableLayoutPanel.Name = "trackedCoinsTableLayoutPanel";
            trackedCoinsTableLayoutPanel.RowCount = 2;
            trackedCoinsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            trackedCoinsTableLayoutPanel.RowStyles.Add(new RowStyle());
            trackedCoinsTableLayoutPanel.Size = new Size(786, 150);
            trackedCoinsTableLayoutPanel.TabIndex = 0;
            // 
            // removeTrackedCoinsButton
            // 
            removeTrackedCoinsButton.Dock = DockStyle.Bottom;
            removeTrackedCoinsButton.Location = new Point(3, 124);
            removeTrackedCoinsButton.Name = "removeTrackedCoinsButton";
            removeTrackedCoinsButton.Size = new Size(780, 23);
            removeTrackedCoinsButton.TabIndex = 0;
            removeTrackedCoinsButton.Text = "Remove Selected Coins From Tracked Coin List";
            removeTrackedCoinsButton.UseVisualStyleBackColor = true;
            removeTrackedCoinsButton.Click += removeTrackedCoinsButton_Click;
            // 
            // trackedCoinsDataGridView
            // 
            trackedCoinsDataGridView.AllowUserToAddRows = false;
            trackedCoinsDataGridView.AllowUserToDeleteRows = false;
            trackedCoinsDataGridView.AllowUserToOrderColumns = true;
            trackedCoinsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            trackedCoinsDataGridView.Dock = DockStyle.Fill;
            trackedCoinsDataGridView.FilterAndSortEnabled = true;
            trackedCoinsDataGridView.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            trackedCoinsDataGridView.Location = new Point(3, 3);
            trackedCoinsDataGridView.MaxFilterButtonImageHeight = 23;
            trackedCoinsDataGridView.Name = "trackedCoinsDataGridView";
            trackedCoinsDataGridView.ReadOnly = true;
            trackedCoinsDataGridView.RightToLeft = RightToLeft.No;
            trackedCoinsDataGridView.Size = new Size(780, 115);
            trackedCoinsDataGridView.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            trackedCoinsDataGridView.TabIndex = 1;
            // 
            // settingsTabPage
            // 
            settingsTabPage.Controls.Add(settingsTabControl);
            settingsTabPage.Location = new Point(4, 24);
            settingsTabPage.Name = "settingsTabPage";
            settingsTabPage.Padding = new Padding(3);
            settingsTabPage.Size = new Size(792, 422);
            settingsTabPage.TabIndex = 1;
            settingsTabPage.Text = "Settings";
            settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(apiSettingsTabPage);
            settingsTabControl.Controls.Add(tabPage3);
            settingsTabControl.Dock = DockStyle.Fill;
            settingsTabControl.Location = new Point(3, 3);
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(786, 416);
            settingsTabControl.TabIndex = 0;
            // 
            // apiSettingsTabPage
            // 
            apiSettingsTabPage.Controls.Add(apiSettingsTabControl);
            apiSettingsTabPage.Location = new Point(4, 24);
            apiSettingsTabPage.Name = "apiSettingsTabPage";
            apiSettingsTabPage.Padding = new Padding(3);
            apiSettingsTabPage.Size = new Size(778, 388);
            apiSettingsTabPage.TabIndex = 0;
            apiSettingsTabPage.Text = "APIs";
            apiSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // apiSettingsTabControl
            // 
            apiSettingsTabControl.Controls.Add(coinDeskSettingsTabPage);
            apiSettingsTabControl.Controls.Add(polygonSettingsTabPage);
            apiSettingsTabControl.Controls.Add(chatGPTSettingsTabPage);
            apiSettingsTabControl.Dock = DockStyle.Fill;
            apiSettingsTabControl.Location = new Point(3, 3);
            apiSettingsTabControl.Name = "apiSettingsTabControl";
            apiSettingsTabControl.SelectedIndex = 0;
            apiSettingsTabControl.Size = new Size(772, 382);
            apiSettingsTabControl.TabIndex = 0;
            // 
            // coinDeskSettingsTabPage
            // 
            coinDeskSettingsTabPage.Controls.Add(coinDeskSettingsTableLayoutPanel);
            coinDeskSettingsTabPage.Location = new Point(4, 24);
            coinDeskSettingsTabPage.Name = "coinDeskSettingsTabPage";
            coinDeskSettingsTabPage.Size = new Size(764, 354);
            coinDeskSettingsTabPage.TabIndex = 2;
            coinDeskSettingsTabPage.Text = "CoinDesk";
            coinDeskSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // coinDeskSettingsTableLayoutPanel
            // 
            coinDeskSettingsTableLayoutPanel.ColumnCount = 1;
            coinDeskSettingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            coinDeskSettingsTableLayoutPanel.Controls.Add(coinDeskApiKeyTableLayoutPanel, 0, 0);
            coinDeskSettingsTableLayoutPanel.Dock = DockStyle.Fill;
            coinDeskSettingsTableLayoutPanel.Location = new Point(0, 0);
            coinDeskSettingsTableLayoutPanel.Name = "coinDeskSettingsTableLayoutPanel";
            coinDeskSettingsTableLayoutPanel.RowCount = 2;
            coinDeskSettingsTableLayoutPanel.RowStyles.Add(new RowStyle());
            coinDeskSettingsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            coinDeskSettingsTableLayoutPanel.Size = new Size(764, 354);
            coinDeskSettingsTableLayoutPanel.TabIndex = 0;
            // 
            // coinDeskApiKeyTableLayoutPanel
            // 
            coinDeskApiKeyTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            coinDeskApiKeyTableLayoutPanel.ColumnCount = 2;
            coinDeskApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            coinDeskApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            coinDeskApiKeyTableLayoutPanel.Controls.Add(label5, 0, 0);
            coinDeskApiKeyTableLayoutPanel.Controls.Add(coinDeskApiKeyTextBox, 1, 0);
            coinDeskApiKeyTableLayoutPanel.Location = new Point(3, 3);
            coinDeskApiKeyTableLayoutPanel.Name = "coinDeskApiKeyTableLayoutPanel";
            coinDeskApiKeyTableLayoutPanel.RowCount = 1;
            coinDeskApiKeyTableLayoutPanel.RowStyles.Add(new RowStyle());
            coinDeskApiKeyTableLayoutPanel.Size = new Size(758, 32);
            coinDeskApiKeyTableLayoutPanel.TabIndex = 0;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(3, 8);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 0;
            label5.Text = "API Key:";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // coinDeskApiKeyTextBox
            // 
            coinDeskApiKeyTextBox.Dock = DockStyle.Fill;
            coinDeskApiKeyTextBox.Location = new Point(59, 3);
            coinDeskApiKeyTextBox.Name = "coinDeskApiKeyTextBox";
            coinDeskApiKeyTextBox.Size = new Size(696, 23);
            coinDeskApiKeyTextBox.TabIndex = 1;
            coinDeskApiKeyTextBox.TextChanged += coinDeskApiKeyTextBox_TextChanged;
            coinDeskApiKeyTextBox.Leave += coinDeskApiKeyTextBox_Leave;
            // 
            // polygonSettingsTabPage
            // 
            polygonSettingsTabPage.Controls.Add(polygonSettingsTableLayoutPanel);
            polygonSettingsTabPage.Location = new Point(4, 24);
            polygonSettingsTabPage.Name = "polygonSettingsTabPage";
            polygonSettingsTabPage.Padding = new Padding(3);
            polygonSettingsTabPage.Size = new Size(764, 354);
            polygonSettingsTabPage.TabIndex = 0;
            polygonSettingsTabPage.Text = "Polygon";
            polygonSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // polygonSettingsTableLayoutPanel
            // 
            polygonSettingsTableLayoutPanel.ColumnCount = 1;
            polygonSettingsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            polygonSettingsTableLayoutPanel.Controls.Add(polygonApiKeyTableLayoutPanel, 0, 0);
            polygonSettingsTableLayoutPanel.Controls.Add(polygonRateLimitGroupBox, 0, 2);
            polygonSettingsTableLayoutPanel.Controls.Add(polygonIgnoreInactiveTickersCheckbox, 0, 1);
            polygonSettingsTableLayoutPanel.Controls.Add(polygonRefreshSettingsGroupBox, 0, 3);
            polygonSettingsTableLayoutPanel.Dock = DockStyle.Fill;
            polygonSettingsTableLayoutPanel.Location = new Point(3, 3);
            polygonSettingsTableLayoutPanel.Name = "polygonSettingsTableLayoutPanel";
            polygonSettingsTableLayoutPanel.RowCount = 4;
            polygonSettingsTableLayoutPanel.RowStyles.Add(new RowStyle());
            polygonSettingsTableLayoutPanel.RowStyles.Add(new RowStyle());
            polygonSettingsTableLayoutPanel.RowStyles.Add(new RowStyle());
            polygonSettingsTableLayoutPanel.RowStyles.Add(new RowStyle());
            polygonSettingsTableLayoutPanel.Size = new Size(758, 348);
            polygonSettingsTableLayoutPanel.TabIndex = 0;
            // 
            // polygonApiKeyTableLayoutPanel
            // 
            polygonApiKeyTableLayoutPanel.AutoSize = true;
            polygonApiKeyTableLayoutPanel.ColumnCount = 5;
            polygonApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            polygonApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            polygonApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            polygonApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            polygonApiKeyTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            polygonApiKeyTableLayoutPanel.Controls.Add(label1, 0, 0);
            polygonApiKeyTableLayoutPanel.Controls.Add(polygonApiKeyTextBox, 1, 0);
            polygonApiKeyTableLayoutPanel.Dock = DockStyle.Fill;
            polygonApiKeyTableLayoutPanel.Location = new Point(3, 3);
            polygonApiKeyTableLayoutPanel.Name = "polygonApiKeyTableLayoutPanel";
            polygonApiKeyTableLayoutPanel.RowCount = 1;
            polygonApiKeyTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            polygonApiKeyTableLayoutPanel.Size = new Size(752, 29);
            polygonApiKeyTableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 7);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "API Key:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // polygonApiKeyTextBox
            // 
            polygonApiKeyTextBox.Dock = DockStyle.Fill;
            polygonApiKeyTextBox.Location = new Point(59, 3);
            polygonApiKeyTextBox.Name = "polygonApiKeyTextBox";
            polygonApiKeyTextBox.Size = new Size(690, 23);
            polygonApiKeyTextBox.TabIndex = 1;
            polygonApiKeyTextBox.TextChanged += polygonApiKeyTextBox_TextChanged;
            polygonApiKeyTextBox.Leave += polygonApiKeyTextBox_Leave;
            // 
            // polygonRateLimitGroupBox
            // 
            polygonRateLimitGroupBox.Controls.Add(polygonQueriesPerMinuteFlowLayoutPanel);
            polygonRateLimitGroupBox.Location = new Point(3, 63);
            polygonRateLimitGroupBox.Name = "polygonRateLimitGroupBox";
            polygonRateLimitGroupBox.Size = new Size(752, 67);
            polygonRateLimitGroupBox.TabIndex = 1;
            polygonRateLimitGroupBox.TabStop = false;
            polygonRateLimitGroupBox.Text = "Rate Limit";
            // 
            // polygonQueriesPerMinuteFlowLayoutPanel
            // 
            polygonQueriesPerMinuteFlowLayoutPanel.AutoSize = true;
            polygonQueriesPerMinuteFlowLayoutPanel.Controls.Add(polygonLimitQueriesPerMinuteCheckbox);
            polygonQueriesPerMinuteFlowLayoutPanel.Controls.Add(polygonQueriesPerMinuteNumericUpDown);
            polygonQueriesPerMinuteFlowLayoutPanel.Controls.Add(label2);
            polygonQueriesPerMinuteFlowLayoutPanel.Dock = DockStyle.Top;
            polygonQueriesPerMinuteFlowLayoutPanel.Location = new Point(3, 19);
            polygonQueriesPerMinuteFlowLayoutPanel.Name = "polygonQueriesPerMinuteFlowLayoutPanel";
            polygonQueriesPerMinuteFlowLayoutPanel.Size = new Size(746, 29);
            polygonQueriesPerMinuteFlowLayoutPanel.TabIndex = 0;
            // 
            // polygonLimitQueriesPerMinuteCheckbox
            // 
            polygonLimitQueriesPerMinuteCheckbox.Anchor = AnchorStyles.Left;
            polygonLimitQueriesPerMinuteCheckbox.AutoSize = true;
            polygonLimitQueriesPerMinuteCheckbox.Location = new Point(3, 5);
            polygonLimitQueriesPerMinuteCheckbox.Name = "polygonLimitQueriesPerMinuteCheckbox";
            polygonLimitQueriesPerMinuteCheckbox.Size = new Size(82, 19);
            polygonLimitQueriesPerMinuteCheckbox.TabIndex = 1;
            polygonLimitQueriesPerMinuteCheckbox.Text = "Throttle to";
            polygonLimitQueriesPerMinuteCheckbox.UseVisualStyleBackColor = true;
            polygonLimitQueriesPerMinuteCheckbox.CheckedChanged += polygonThrottlingSettingsUpdated;
            // 
            // polygonQueriesPerMinuteNumericUpDown
            // 
            polygonQueriesPerMinuteNumericUpDown.Location = new Point(91, 3);
            polygonQueriesPerMinuteNumericUpDown.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            polygonQueriesPerMinuteNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            polygonQueriesPerMinuteNumericUpDown.Name = "polygonQueriesPerMinuteNumericUpDown";
            polygonQueriesPerMinuteNumericUpDown.Size = new Size(63, 23);
            polygonQueriesPerMinuteNumericUpDown.TabIndex = 2;
            polygonQueriesPerMinuteNumericUpDown.TextAlign = HorizontalAlignment.Right;
            polygonQueriesPerMinuteNumericUpDown.Value = new decimal(new int[] { 5, 0, 0, 0 });
            polygonQueriesPerMinuteNumericUpDown.ValueChanged += polygonThrottlingSettingsUpdated;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(160, 7);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 3;
            label2.Text = "queries per minute";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // polygonIgnoreInactiveTickersCheckbox
            // 
            polygonIgnoreInactiveTickersCheckbox.AutoSize = true;
            polygonIgnoreInactiveTickersCheckbox.Checked = true;
            polygonIgnoreInactiveTickersCheckbox.CheckState = CheckState.Checked;
            polygonIgnoreInactiveTickersCheckbox.Location = new Point(3, 38);
            polygonIgnoreInactiveTickersCheckbox.Name = "polygonIgnoreInactiveTickersCheckbox";
            polygonIgnoreInactiveTickersCheckbox.Size = new Size(144, 19);
            polygonIgnoreInactiveTickersCheckbox.TabIndex = 2;
            polygonIgnoreInactiveTickersCheckbox.Text = "Ignore Inactive TIckers";
            polygonIgnoreInactiveTickersCheckbox.UseVisualStyleBackColor = true;
            polygonIgnoreInactiveTickersCheckbox.CheckedChanged += polygonIgnoreInactiveTickersCheckbox_CheckedChanged;
            // 
            // polygonRefreshSettingsGroupBox
            // 
            polygonRefreshSettingsGroupBox.AutoSize = true;
            polygonRefreshSettingsGroupBox.Controls.Add(tickerListRefreshFlowLayoutPanel);
            polygonRefreshSettingsGroupBox.Dock = DockStyle.Fill;
            polygonRefreshSettingsGroupBox.Location = new Point(3, 136);
            polygonRefreshSettingsGroupBox.Name = "polygonRefreshSettingsGroupBox";
            polygonRefreshSettingsGroupBox.Size = new Size(752, 209);
            polygonRefreshSettingsGroupBox.TabIndex = 3;
            polygonRefreshSettingsGroupBox.TabStop = false;
            polygonRefreshSettingsGroupBox.Text = "Refresh";
            // 
            // tickerListRefreshFlowLayoutPanel
            // 
            tickerListRefreshFlowLayoutPanel.AutoSize = true;
            tickerListRefreshFlowLayoutPanel.Controls.Add(label3);
            tickerListRefreshFlowLayoutPanel.Controls.Add(tickerListRefreshIntervalDaysUpDown);
            tickerListRefreshFlowLayoutPanel.Controls.Add(label4);
            tickerListRefreshFlowLayoutPanel.Controls.Add(refreshTickerListButton);
            tickerListRefreshFlowLayoutPanel.Controls.Add(tickerListLastRefreshedLabel);
            tickerListRefreshFlowLayoutPanel.Dock = DockStyle.Top;
            tickerListRefreshFlowLayoutPanel.Location = new Point(3, 19);
            tickerListRefreshFlowLayoutPanel.Name = "tickerListRefreshFlowLayoutPanel";
            tickerListRefreshFlowLayoutPanel.Size = new Size(746, 44);
            tickerListRefreshFlowLayoutPanel.TabIndex = 0;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 7);
            label3.Name = "label3";
            label3.Size = new Size(127, 15);
            label3.TabIndex = 0;
            label3.Text = "Refresh ticker list every";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tickerListRefreshIntervalDaysUpDown
            // 
            tickerListRefreshIntervalDaysUpDown.Location = new Point(136, 3);
            tickerListRefreshIntervalDaysUpDown.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            tickerListRefreshIntervalDaysUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            tickerListRefreshIntervalDaysUpDown.Name = "tickerListRefreshIntervalDaysUpDown";
            tickerListRefreshIntervalDaysUpDown.Size = new Size(48, 23);
            tickerListRefreshIntervalDaysUpDown.TabIndex = 1;
            tickerListRefreshIntervalDaysUpDown.TextAlign = HorizontalAlignment.Right;
            tickerListRefreshIntervalDaysUpDown.Value = new decimal(new int[] { 30, 0, 0, 0 });
            tickerListRefreshIntervalDaysUpDown.ValueChanged += tickerListRefreshIntervalDaysUpDown_ValueChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(190, 7);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 2;
            label4.Text = "days";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // refreshTickerListButton
            // 
            tickerListRefreshFlowLayoutPanel.SetFlowBreak(refreshTickerListButton, true);
            refreshTickerListButton.Location = new Point(227, 3);
            refreshTickerListButton.Name = "refreshTickerListButton";
            refreshTickerListButton.Size = new Size(101, 23);
            refreshTickerListButton.TabIndex = 3;
            refreshTickerListButton.Text = "Refresh Now";
            refreshTickerListButton.UseVisualStyleBackColor = true;
            refreshTickerListButton.Click += refreshTickerListButton_Click;
            // 
            // tickerListLastRefreshedLabel
            // 
            tickerListLastRefreshedLabel.Anchor = AnchorStyles.Left;
            tickerListLastRefreshedLabel.AutoSize = true;
            tickerListLastRefreshedLabel.Location = new Point(3, 29);
            tickerListLastRefreshedLabel.Name = "tickerListLastRefreshedLabel";
            tickerListLastRefreshedLabel.Size = new Size(134, 15);
            tickerListLastRefreshedLabel.TabIndex = 4;
            tickerListLastRefreshedLabel.Text = "last refreshed: Unknown";
            tickerListLastRefreshedLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chatGPTSettingsTabPage
            // 
            chatGPTSettingsTabPage.Location = new Point(4, 24);
            chatGPTSettingsTabPage.Name = "chatGPTSettingsTabPage";
            chatGPTSettingsTabPage.Padding = new Padding(3);
            chatGPTSettingsTabPage.Size = new Size(764, 354);
            chatGPTSettingsTabPage.TabIndex = 1;
            chatGPTSettingsTabPage.Text = "ChatGPT";
            chatGPTSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(778, 388);
            tabPage3.TabIndex = 1;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(mainTabControl);
            Name = "MainForm";
            Text = "AI Trading Assistant";
            FormClosed += MainForm_FormClosed;
            mainTabControl.ResumeLayout(false);
            coinListTabPage.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            allCoinsTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)allCoinsDataGridView).EndInit();
            trackedCoinsTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackedCoinsDataGridView).EndInit();
            settingsTabPage.ResumeLayout(false);
            settingsTabControl.ResumeLayout(false);
            apiSettingsTabPage.ResumeLayout(false);
            apiSettingsTabControl.ResumeLayout(false);
            coinDeskSettingsTabPage.ResumeLayout(false);
            coinDeskSettingsTableLayoutPanel.ResumeLayout(false);
            coinDeskApiKeyTableLayoutPanel.ResumeLayout(false);
            coinDeskApiKeyTableLayoutPanel.PerformLayout();
            polygonSettingsTabPage.ResumeLayout(false);
            polygonSettingsTableLayoutPanel.ResumeLayout(false);
            polygonSettingsTableLayoutPanel.PerformLayout();
            polygonApiKeyTableLayoutPanel.ResumeLayout(false);
            polygonApiKeyTableLayoutPanel.PerformLayout();
            polygonRateLimitGroupBox.ResumeLayout(false);
            polygonRateLimitGroupBox.PerformLayout();
            polygonQueriesPerMinuteFlowLayoutPanel.ResumeLayout(false);
            polygonQueriesPerMinuteFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)polygonQueriesPerMinuteNumericUpDown).EndInit();
            polygonRefreshSettingsGroupBox.ResumeLayout(false);
            polygonRefreshSettingsGroupBox.PerformLayout();
            tickerListRefreshFlowLayoutPanel.ResumeLayout(false);
            tickerListRefreshFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tickerListRefreshIntervalDaysUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl mainTabControl;
        private TabPage coinListTabPage;
        private TabPage settingsTabPage;
        private TabControl settingsTabControl;
        private TabPage apiSettingsTabPage;
        private TabControl apiSettingsTabControl;
        private TabPage polygonSettingsTabPage;
        private TabPage chatGPTSettingsTabPage;
        private TabPage tabPage3;
        private TableLayoutPanel polygonSettingsTableLayoutPanel;
        private TableLayoutPanel polygonApiKeyTableLayoutPanel;
        private Label label1;
        private TextBox polygonApiKeyTextBox;
        private GroupBox polygonRateLimitGroupBox;
        private FlowLayoutPanel polygonQueriesPerMinuteFlowLayoutPanel;
        private CheckBox polygonLimitQueriesPerMinuteCheckbox;
        private NumericUpDown polygonQueriesPerMinuteNumericUpDown;
        private Label label2;
        private SplitContainer splitContainer1;
        private TableLayoutPanel allCoinsTableLayoutPanel;
        private Button addCoinsToTrackedButton;
        private TableLayoutPanel trackedCoinsTableLayoutPanel;
        private Button removeTrackedCoinsButton;
        private CheckBox polygonIgnoreInactiveTickersCheckbox;
        private GroupBox polygonRefreshSettingsGroupBox;
        private FlowLayoutPanel tickerListRefreshFlowLayoutPanel;
        private Label label3;
        private NumericUpDown tickerListRefreshIntervalDaysUpDown;
        private Label label4;
        private Button refreshTickerListButton;
        private Label tickerListLastRefreshedLabel;
        private Zuby.ADGV.AdvancedDataGridView allCoinsDataGridView;
        private Zuby.ADGV.AdvancedDataGridView trackedCoinsDataGridView;
        private TabPage coinDeskSettingsTabPage;
        private TableLayoutPanel coinDeskSettingsTableLayoutPanel;
        private TableLayoutPanel coinDeskApiKeyTableLayoutPanel;
        private Label label5;
        private TextBox coinDeskApiKeyTextBox;
    }
}
