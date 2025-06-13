using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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
        }

        private void startTickerListRefresher()
        {
            tickerListRefresher_ = new CoinMetadataRefresher(new RefreshOptions
            {
                refreshEnabled = true,
                refreshInterval = TimeSpan.FromDays(settings_.polygonTickerRefreshDays),
                constructorWaitsForFirstLoad = true,
                removeMissingElementsAfterReload = true,
            });
            tickerListRefresher_.OnDataReloaded += TickerListRefresher__OnDataReloaded;
            allCoinsDataGridView.AutoGenerateColumns = true;
            allCoinsDataGridView.DataSource = tickerListRefresher_.bindingList;
            allCoinsDataGridView.Refresh();
            TrackedCoinList.instance = TrackedCoinList.loadFromDefaultLocation(tickerListRefresher_);
            trackedCoinsDataGridView.DataSource = TrackedCoinList.instance.coinMetadataBinding;
        }

        private void TickerListRefresher__OnDataReloaded(RefreshableDataRecordsInterface sender, RefreshableDataRecords<CoinMetadata, string>.DataReloadedEventArgs args)
        {
            tickerListRefresher_.ResetBindingsOnControlThread(allCoinsDataGridView);
        }

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
            PropertyDescriptorCollection propDescriptors = TypeDescriptor.GetProperties(typeof(JsonResponses.CoinMetadata));
            string propertyName = column.DataPropertyName;
            AttributeCollection attributes = propDescriptors.Find(propertyName, false).Attributes;
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
        private CoinMetadataRefresher tickerListRefresher_;

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
            Task.Run(async () => tickerListRefresher_.startRefresh());
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
                TrackedCoinList.instance.addCoinId(((CoinMetadata)row.DataBoundItem).ticker);
            }
        }

        private void removeTrackedCoinsButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in trackedCoinsDataGridView.SelectedRows)
            {
                TrackedCoinList.instance.removeCoinId(((CoinMetadata)row.DataBoundItem).ticker);
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
    }
}
