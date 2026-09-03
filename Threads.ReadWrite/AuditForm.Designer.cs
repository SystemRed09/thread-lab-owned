namespace Threads.ReadWrite
{
    partial class AuditForm
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
            this.explainLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // explainLabel
            //
            this.explainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.explainLabel.Location = new System.Drawing.Point(12, 9);
            this.explainLabel.Name = "explainLabel";
            this.explainLabel.Size = new System.Drawing.Size(400, 104);
            this.explainLabel.TabIndex = 0;
            this.explainLabel.Text = "Hand-testing exposes the missing state check. It cannot expose the race that " +
                "survives it: two threads both reading \"Closed\" and both opening the file.\r\n\r\n" +
                "This runs 4 threads through 20,000 open/close cycles each, on its own private " +
                "file, and counts the moments when two of them held it at once.\r\n\r\n" +
                "Fix FileController until VIOLATIONS reads 0.";
            //
            // runButton
            //
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.runButton.Location = new System.Drawing.Point(12, 122);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(140, 30);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run Self-Test";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            //
            // outputBox
            //
            this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.outputBox.Location = new System.Drawing.Point(12, 162);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(400, 220);
            this.outputBox.TabIndex = 2;
            //
            // AuditForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 394);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.explainLabel);
            this.MinimumSize = new System.Drawing.Size(440, 360);
            this.Name = "AuditForm";
            this.Text = "Self-Test";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label explainLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.TextBox outputBox;
    }
}
