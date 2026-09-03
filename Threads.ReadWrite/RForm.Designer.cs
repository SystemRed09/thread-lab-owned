namespace Threads.ReadWrite
{
    partial class RForm
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
            this.openButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.contentLabel = new System.Windows.Forms.Label();
            this.contentBox = new System.Windows.Forms.TextBox();
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
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 72);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(120, 34);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(150, 72);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(120, 34);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Read Next Line";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(288, 72);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 34);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // contentLabel
            // 
            this.contentLabel.Location = new System.Drawing.Point(12, 118);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(396, 20);
            this.contentLabel.TabIndex = 5;
            this.contentLabel.Text = "Lines read in this session:";
            // 
            // contentBox
            // 
            this.contentBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentBox.BackColor = System.Drawing.SystemColors.Window;
            this.contentBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.contentBox.Location = new System.Drawing.Point(12, 142);
            this.contentBox.Multiline = true;
            this.contentBox.Name = "contentBox";
            this.contentBox.ReadOnly = true;
            this.contentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentBox.Size = new System.Drawing.Size(396, 238);
            this.contentBox.TabIndex = 6;
            // 
            // RForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 392);
            this.Controls.Add(this.contentBox);
            this.Controls.Add(this.contentLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.stateLabel);
            this.MinimumSize = new System.Drawing.Size(438, 300);
            this.Name = "RForm";
            this.Text = "Reader";
            this.Load += new System.EventHandler(this.RForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.TextBox contentBox;
    }
}
