namespace TradingAssistant
{
    partial class SaveRevertDialog
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
            messageLabel = new Label();
            saveButton = new Button();
            revertButton = new Button();
            testButton = new Button();
            SuspendLayout();
            // 
            // messageLabel
            // 
            messageLabel.AutoSize = true;
            messageLabel.Location = new Point(12, 9);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(53, 15);
            messageLabel.TabIndex = 0;
            messageLabel.Text = "message";
            // 
            // saveButton
            // 
            saveButton.Location = new Point(414, 127);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 1;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // revertButton
            // 
            revertButton.Location = new Point(495, 127);
            revertButton.Name = "revertButton";
            revertButton.Size = new Size(75, 23);
            revertButton.TabIndex = 2;
            revertButton.Text = "Revert";
            revertButton.UseVisualStyleBackColor = true;
            revertButton.Click += revertButton_Click;
            // 
            // testButton
            // 
            testButton.Location = new Point(322, 129);
            testButton.Name = "testButton";
            testButton.Size = new Size(86, 21);
            testButton.TabIndex = 3;
            testButton.Text = "Test";
            testButton.UseVisualStyleBackColor = true;
            testButton.Click += testButton_Click;
            // 
            // SaveRevertDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 162);
            Controls.Add(testButton);
            Controls.Add(revertButton);
            Controls.Add(saveButton);
            Controls.Add(messageLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SaveRevertDialog";
            Text = "SaveRevertDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label messageLabel;
        private Button saveButton;
        private Button revertButton;
        private Button testButton;
    }
}