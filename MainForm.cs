using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Forms;
using TradingAssistant.DataFetchers;
using TradingAssistant.JsonResponses;

namespace TradingAssistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            initSettingsInputs();
            startTickerListRefresher();
            marketOrdering_ = MarketOrdering.loadFromDefaultPathIfExists();
            refreshMarketList();
        }

        private void startTickerListRefresher()
        {
            //polygonTickerListRefresher_ = new CoinMetadataRefresher(new RefreshOptions
            //{
            //    refreshEnabled = true,
            //    refreshInterval = TimeSpan.FromDays(settings_.polygonTickerRefreshDays),
            //    constructorWaitsForFirstLoad = true,
            //    removeMissingElementsAfterReload = true,
            //});
            //polygonTickerListRefresher_.OnDataReloaded += TickerListRefresher__OnDataReloaded;

            coinDeskTopListRefresher_ = new CoinDeskTopListRefresher(new RefreshOptions
            {
                refreshEnabled = true,
                refreshInterval = TimeSpan.FromDays(1),
                constructorWaitsForFirstLoad = true,
                removeMissingElementsAfterReload = true,
            });
            coinDeskTopListRefresher_.OnDataReloaded += CoinDeskTopListRefresher__OnDataReloaded;

            allCoinsDataGridView.AutoGenerateColumns = true;
            allCoinsDataGridView.DataSource = coinDeskTopListRefresher_.bindingList;
            // polygonTickerListRefresher_.bindingList;
            allCoinsDataGridView.Refresh();
            trackedCoinList = TrackedCoinList.loadFromDefaultLocation(coinDeskTopListRefresher_);
            trackedCoinsDataGridView.DataSource = trackedCoinList.coinMetadataBinding;
        }

        private TrackedCoinList trackedCoinList
        {
            get { return TrackedCoinList.instance; }
            set { TrackedCoinList.instance = value; }
        }

        private void CoinDeskTopListRefresher__OnDataReloaded(RefreshableDataRecordsInterface sender, RefreshableDataRecords<CoinDeskTopList.Item, int>.DataReloadedEventArgs args)
        {
            coinDeskTopListRefresher_.ResetBindingsOnControlThread(allCoinsDataGridView);
        }

        //private void TickerListRefresher__OnDataReloaded(RefreshableDataRecordsInterface sender, RefreshableDataRecords<CoinMetadata, string>.DataReloadedEventArgs args)
        //{
        //    polygonTickerListRefresher_.ResetBindingsOnControlThread(allCoinsDataGridView);
        //}

        private void initSettingsInputs()
        {
            PolygonApi.updateThrottlingInterval();
            polygonApiKeyTextBox.Text = settings_.polygonApiKey;
            coinDeskApiKeyTextBox.Text = settings_.coinDeskApiKey;
            polygonLimitQueriesPerMinuteCheckbox.Checked = settings_.polygonThrottlingEnabled;
            polygonQueriesPerMinuteNumericUpDown.Value = settings_.polygonThrottlingQueriesPerMinute;
            polygonIgnoreInactiveTickersCheckbox.Checked = settings_.polygonIgnoreInactiveTickers;
            tickerListRefreshIntervalDaysUpDown.Value = (decimal)settings_.polygonTickerRefreshDays;
            settingInputsInited_ = true;
        }

        private void maybeSaveSettings()
        {
            if (settingInputsInited_)
            {
                settings_.Save();
            }
        }

        internal void polygonThrottlingSettingsUpdated(object sender, EventArgs e)
        {
            settings_.polygonThrottlingEnabled = polygonLimitQueriesPerMinuteCheckbox.Checked;
            maybeSaveSettings();
        }

        private Properties.Settings settings_ = Properties.Settings.Default;
        private bool polygonApiKeyDirty_ = false;
        private bool coinDeskApiKeyDirty_ = false;

        private void polygonApiKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            polygonApiKeyDirty_ = polygonApiKeyTextBox.Text != settings_.polygonApiKey;
        }
        private void polygonApiKeyTestButton_Click(object sender, EventArgs e)
        {
            bool passed = false;
            try
            {
                passed = PolygonApi.testApiKey(polygonApiKeyTextBox.Text);
            }
            catch (Exception ex) { }
            if (polygonApiKeyDirty_)
            {
                if (passed)
                {
                    DialogResult dr = MessageBox.Show("Polygon API test succeeded.  Save the new API key?", "SUCCESS", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        savePolygonApiKey();
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Polygon API test FAILED!  Revert the API key input?", "FAILURE", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        revertPolygonApiKey();
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("Polygon API test {0}", passed ? "PASSED" : "FAILED"));
            }
        }

        private void polygonApiKeyTextBox_Leave(object sender, EventArgs e)
        {
            if (!polygonApiKeyDirty_ || polygonApiKeyTextBox.Text == settings_.polygonApiKey) return;
            bool passed = false;
            try
            {
                passed = PolygonApi.testApiKey(polygonApiKeyTextBox.Text);
            }
            catch (Exception ex) { }
            if (!passed)
            {
                DialogResult dr = MessageBox.Show("The polygon API key doesn't seem to work.  Would you like to revert it?", "Invalid Polygon API Key", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    revertPolygonApiKey();
                }
                else
                {
                    savePolygonApiKey();
                }
            }
        }

        private void savePolygonApiKey()
        {
            settings_.polygonApiKey = polygonApiKeyTextBox.Text;
            maybeSaveSettings();
            polygonApiKeyDirty_ = false;
        }

        private void revertPolygonApiKey()
        {
            polygonApiKeyTextBox.Text = settings_.polygonApiKey;
            polygonApiKeyDirty_ = false;
        }



        private void DataGridViewColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var dgv = sender as DataGridView;
            Type rowType = dgv.DataSource.GetType();
            DataGridViewColumn column = e.Column;
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            PropertyDescriptorCollection propDescriptors = TypeDescriptor.GetProperties(typeof(JsonResponses.CoinDeskTopList.Item));
            string propertyName = column.DataPropertyName;
            AttributeCollection? attributes = propDescriptors.Find(propertyName, ignoreCase: true)?.Attributes;
            if (attributes == null) return;
            var displayAttr = attributes.OfType<DisplayAttribute>().FirstOrDefault();
            column.Visible = displayAttr?.GetAutoGenerateField() ?? true;
            column.Name = displayAttr?.Name ?? propertyName;
            var displayFormatAttribute = attributes.OfType<DisplayFormatAttribute>().FirstOrDefault();
            if (displayFormatAttribute?.DataFormatString != null)
            {
                column.DefaultCellStyle.Format = displayFormatAttribute.DataFormatString;
            }
            if (e.Column.ValueType?.IsAssignableTo(typeof(double)) ?? false)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void polygonIgnoreInactiveTickersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            settings_.polygonIgnoreInactiveTickers = polygonIgnoreInactiveTickersCheckbox.Checked;
            maybeSaveSettings();
        }

        bool settingInputsInited_ = false;
        private CoinMetadataRefresher polygonTickerListRefresher_;
        private CoinDeskTopListRefresher coinDeskTopListRefresher_;
        private MarketOrdering marketOrdering_;

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshableDataRecordsInterface.disposeAllRefreshers();
        }

        private void tickerListRefreshIntervalDaysUpDown_ValueChanged(object sender, EventArgs e)
        {
            settings_.polygonTickerRefreshDays = (double)tickerListRefreshIntervalDaysUpDown.Value;
            maybeSaveSettings();
        }

        private void refreshTickerListButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () => polygonTickerListRefresher_.startRefresh());
            allCoinsDataGridView.Refresh();
        }

        private void allCoinsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Console.WriteLine("allCoinsDataGridView_DataBindingComplete: sender: {0}, e: {1}", sender, e);
        }

        private void addCoinsToTrackedButton_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = allCoinsDataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                trackedCoinList.addCoinId(((CoinDeskTopList.Item)row.DataBoundItem).ID);
            }
        }

        private void removeTrackedCoinsButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in trackedCoinsDataGridView.SelectedRows)
            {
                trackedCoinList.removeCoinId(((CoinDeskTopList.Item)row.DataBoundItem).ID);
            }
        }

        private void coinDeskApiKeyTextBox_Leave(object sender, EventArgs e)
        {
            if (!coinDeskApiKeyDirty_) return;
            bool passed = false;
            try
            {
                passed = CoinDeskApi.testApiKey(coinDeskApiKeyTextBox.Text);
            }
            catch (Exception ex) { }
            if (!passed)
            {
                DialogResult dr = MessageBox.Show("The CoinDesk API key doesn't seem to work.  Would you like to revert it?", "Invalid CoinDesk API Key", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    revertCoinDeskApiKey();
                }
                else
                {
                    saveCoinDeskApiKey();
                }
            }
        }

        private void saveCoinDeskApiKey()
        {
            settings_.coinDeskApiKey = coinDeskApiKeyTextBox.Text;
            maybeSaveSettings();
            coinDeskApiKeyDirty_ = false;
        }

        private void revertCoinDeskApiKey()
        {
            coinDeskApiKeyTextBox.Text = settings_.coinDeskApiKey;
            coinDeskApiKeyDirty_ = false;
        }

        private void coinDeskApiKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            coinDeskApiKeyDirty_ = coinDeskApiKeyTextBox.Text != settings_.coinDeskApiKey;
        }

        private void coinDeskMarketPriorityGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void coinDeskMarketsListBox_DragOver(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Move;
            //marketOrdering_.CopyOrderingFromCollection(coinDeskMarketsListBox.Items);
            //coinDeskMarketsListBox.Invalidate();
        }

        private void coinDeskMarketsListBox_DragDrop(object sender, DragEventArgs e)
        {
            //Point point = coinDeskMarketsListBox.PointToClient(new Point(e.X, e.Y));
            //int index = this.coinDeskMarketsListBox.IndexFromPoint(point);
            //if (index < 0) index = this.coinDeskMarketsListBox.Items.Count - 1;
            //object data = e.Data.GetData(typeof(MarketOrderingItem));
            //this.coinDeskMarketsListBox.Items.Remove(data);
            //this.coinDeskMarketsListBox.Items.Insert(index, data);
            //marketOrdering_.CopyOrderingFromCollection(coinDeskMarketsListBox.Items);
            //coinDeskMarketsListBox.Invalidate();
        }

        private void refreshMarketListButton_Click(object sender, EventArgs e)
        {
            refreshMarketList();
        }

        private void refreshMarketList()
        {
            HashSet<string> markets = AsyncToSyncWrapper.callAsyncAsSync<HashSet<string>>(
                async () => await CoinDeskApi.getActiveMarkets());
            marketOrdering_.processUpdatedMarketList(markets);
            coinDeskMarketsListBox.Items.Clear();
            coinDeskMarketsListBox.Items.AddRange(marketOrdering_.ToArray());
            marketOrdering_.saveToDefaultPath();
        }

        private void coinDeskMarketsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            MarketOrderingItem item = coinDeskMarketsListBox.Items[e.Index] as MarketOrderingItem;
            if (!item.enabled)
            {
                e.Graphics.DrawString(
                    item.name, new Font(e.Font, e.Font.Style | FontStyle.Strikeout),
                    Brushes.Black, e.Bounds);
            }
        }

        private void enableMarketsButton_Click(object sender, EventArgs e)
        {
            foreach (var obj in coinDeskMarketsListBox.SelectedItems)
            {
                var item = obj as MarketOrderingItem;
                item.enabled = true;
            }
            marketOrdering_.saveToDefaultPath();
            coinDeskMarketsListBox.Invalidate();
        }

        private void disableSelectedMarketsButton_Click(object sender, EventArgs e)
        {
            foreach (var obj in coinDeskMarketsListBox.SelectedItems)
            {
                var item = obj as MarketOrderingItem;
                item.enabled = false;
            }
            marketOrdering_.saveToDefaultPath();
            coinDeskMarketsListBox.Invalidate();
        }

        private void coinDeskMarketsListBox_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    // Start the drag operation
            //    int index = coinDeskMarketsListBox.IndexFromPoint(e.Location);
            //    if (index != -1)
            //    {
            //        coinDeskMarketsListBox.SelectedIndex = index;
            //        // Initiate the drag operation
            //        DoDragDrop(coinDeskMarketsListBox.SelectedItems, DragDropEffects.Move);
            //    }
            //}
            //marketOrdering_.CopyOrderingFromCollection(coinDeskMarketsListBox.Items);
            //marketOrdering_.saveToDefaultPath();
        }
    }
}
