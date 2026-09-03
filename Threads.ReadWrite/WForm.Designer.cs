namespace Threads.ReadWrite
{
    partial class WForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.stateLabel = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.inputLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.openButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.logLabel = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // stateLabel
            // 
            this.stateLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.stateLabel.Location = new System.Drawing.Point(12, 12);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(396, 24);
            this.stateLabel.TabIndex = 0;
            this.stateLabel.Text = "State: Closed";
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 38);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(396, 24);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "Press Open to ask the controller for the file.";
            // 
            // inputLabel
            // 
            this.inputLabel.Location = new System.Drawing.Point(12, 72);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(396, 20);
            this.inputLabel.TabIndex = 2;
            this.inputLabel.Text = "Line to write (then press Write Line, or Enter):";
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 94);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(396, 25);
            this.inputBox.TabIndex = 3;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 130);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(120, 34);
            this.openButton.TabIndex = 4;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(150, 130);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(120, 34);
            this.writeButton.TabIndex = 5;
            this.writeButton.Text = "Write Line";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(288, 130);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 34);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // logLabel
            // 
            this.logLabel.Location = new System.Drawing.Point(12, 176);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(396, 20);
            this.logLabel.TabIndex = 7;
            this.logLabel.Text = "What this form has done:";
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.BackColor = System.Drawing.SystemColors.Window;
            this.logBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.logBox.Location = new System.Drawing.Point(12, 200);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(396, 180);
            this.logBox.TabIndex = 8;
            // 
            // WForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 392);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.stateLabel);
            this.MinimumSize = new System.Drawing.Size(438, 300);
            this.Name = "WForm";
            this.Text = "Writer";
            this.Load += new System.EventHandler(this.WForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.TextBox logBox;
    }
}
