using System;
using System.Windows.Forms;
using Threads.Core;

namespace Threads.ReadWrite
{
    // Form that opens the shared file for writing, appends lines to it, and closes it.
    public partial class WForm : Form
    {
        private FileController c;  // we connect directly to the controller
        private Writer file;       // handle to the file we will be writing
        private Status state = Status.Closed;

        public WForm(FileController c)
        {
            this.c = c;
            InitializeComponent();
            WindowLayout.PlaceWriter(this);
        }

        private void WForm_Load(object sender, EventArgs e)
        {
            ShowState();
        }

        // Ask the controller for permission to write.
        private void openButton_Click(object sender, EventArgs e)
        {
            file = c.openWrite();

            if (file != null)
            {
                state = Status.Writing;
                Say("Opened for writing.", false);
                Log("--- opened for writing ---");
            }
            else
            {
                Say("Refused. Someone else is using the file.", true);
            }
            ShowState();
            if (state == Status.Writing) { inputBox.Focus(); }
        }

        // Append whatever is in the input box as one line.
        private void writeButton_Click(object sender, EventArgs e)
        {
            if (state != Status.Writing) { return; }

            string line = inputBox.Text;
            if (line.Length == 0)
            {
                Say("Type something in the box first, then press Write Line.", true);
                return;
            }

            file.writeLine(line);
            Log("wrote: " + line);
            Say("Wrote one line.", false);
            inputBox.Text = "";
            inputBox.Focus();
        }

        // Give the file back so another thread may open it.
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (state != Status.Writing) { return; }

            c.close();
            state = Status.Closed;
            file = null;
            Log("--- closed ---");
            Say("Closed. The file is available again.", false);
            ShowState();
        }

        // Pressing Enter in the text box writes the line.
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                writeButton_Click(sender, EventArgs.Empty);
            }
        }

        // Enables exactly the buttons that make sense right now.
        private void ShowState()
        {
            stateLabel.Text = "State: " + state.ToString();

            bool open = (state == Status.Writing);
            openButton.Enabled = !open;
            writeButton.Enabled = open;
            closeButton.Enabled = open;
            inputBox.Enabled = open;
        }

        private void Say(string message, bool isProblem)
        {
            messageLabel.Text = message;
            messageLabel.ForeColor = isProblem
                ? System.Drawing.Color.Firebrick
                : System.Drawing.Color.DarkGreen;
        }

        private void Log(string entry)
        {
            logBox.AppendText(entry + Environment.NewLine);
        }
    }
}
