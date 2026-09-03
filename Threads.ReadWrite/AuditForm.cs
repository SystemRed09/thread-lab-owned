using System;
using System.Threading;
using System.Windows.Forms;
using Threads.Core;

namespace Threads.ReadWrite
{
    // TEST SCAFFOLDING. Runs SelfTest and reports whether two threads were ever able
    // to hold a file at the same moment.
    //
    // The test builds its own File and FileController, so it does not matter what
    // the Reader and Writer forms are doing while it runs.
    //
    // You do not need to modify this form, and it does NOT belong in your class diagram.
    public partial class AuditForm : Form
    {
        private const int THREADS = 4;
        private const int CYCLES = 20000;

        public AuditForm()
        {
            InitializeComponent();
            WindowLayout.PlaceAudit(this);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            runButton.Enabled = false;
            outputBox.Text = "Running " + THREADS + " threads x " + CYCLES.ToString("N0") + " cycles ...";

            // Run off the UI thread so the form stays responsive:
            Thread t = new Thread(delegate()
            {
                SelfTestResult r = SelfTest.Run(THREADS, CYCLES);
                string text = r.ToString();

                // Hop back to this form's UI thread before touching a control:
                outputBox.BeginInvoke(new Action(delegate()
                {
                    outputBox.Text = text.Replace("\n", "\r\n");
                    runButton.Enabled = true;
                }));
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
