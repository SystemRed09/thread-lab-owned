using System;
using System.Windows.Forms;
using Threads.Core;

namespace Threads.ReadWrite
{
    // Form that opens the shared file for reading, reads it one line at a time,
    // and closes it.
    public partial class RForm : Form
    {
        private FileController c;  // we let the form talk directly to the controller
        private Reader file;       // handle to the file we will be reading
        private Status state;      // remembers whether this form is mid-read

        public RForm(FileController c)
        {
            this.c = c;
            state = Status.Closed;
            InitializeComponent();
            WindowLayout.PlaceReader(this);
        }

        private void RForm_Load(object sender, EventArgs e)
        {
            ShowState();
        }

        // Ask the controller for permission to read.
        private void openButton_Click(object sender, EventArgs e)
        {
            file = c.openRead();

            if (file != null)
            {
                state = Status.Reading;
                contentBox.Text = "";
                Say("Opened for reading. Press Read Next Line.", false);
                Log("--- opened for reading ---");
            }
            else
            {
                Say("Refused. Someone else is using the file.", true);
            }
            ShowState();
        }

        // Read one more line from wherever the cursor currently is.
        private void readButton_Click(object sender, EventArgs e)
        {
            if (state != Status.Reading) { return; }

            string line = file.readLine();

            if (line != null)
            {
                Log(line);
                Say("Read one line.", false);
            }
            else
            {
                Log("[EOF]");
                Say("End of file. Nothing more to read from here.", true);
            }
        }

        // Give the file back so another thread may open it.
        private void closeButton_Click(object sender, EventArgs e)
        {
            if (state != Status.Reading) { return; }

            c.close();
            state = Status.Closed;
            file = null;
            Log("--- closed ---");
            Say("Closed. The file is available again.", false);
            ShowState();
        }

        // Enables exactly the buttons that make sense right now.
        private void ShowState()
        {
            stateLabel.Text = "State: " + state.ToString();

            bool open = (state == Status.Reading);
            openButton.Enabled = !open;
            readButton.Enabled = open;
            closeButton.Enabled = open;
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
            contentBox.AppendText(entry + Environment.NewLine);
        }
    }
}
