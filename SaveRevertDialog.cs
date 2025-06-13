using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingAssistant
{
    public partial class SaveRevertDialog : Form
    {
        public SaveRevertDialog()
        {
            InitializeComponent();
        }

        public SaveRevertDialog(string labelText, string caption, Func<object>? testButtonAction) : this()
        {
            messageLabel.Text = labelText;
            Text = caption;
            testButtonAction_ = testButtonAction;
            testButton.Visible = testButtonAction_ != null;
        }

        private Func<object>? testButtonAction_;

        private void testButton_Click(object sender, EventArgs e)
        {
            if (testButtonAction_ != null)
            {
                testButtonAction_.Invoke();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }
    }
}
