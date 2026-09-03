using System;
using System.Threading;
using System.Windows.Forms;
using Threads.Core;

namespace Threads.ReadWrite
{
    static class Program
    {
        // Constructs a "file" that is shared by a reader thread and a writer thread,
        // plus an audit thread that can stress-test the controller on demand.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            File f = new File();
            FileController c = new FileController(f);  // controls access to File f

            // Each form runs on its own thread, with its own call stack:
            new Thread(ReaderThread).Start(c);
            new Thread(WriterThread).Start(c);
            new Thread(AuditThread).Start();

            // the main thread is still running:
            MessageBox.Show("System initiated.");
        }

        // You can pass an object to a new thread (but be sure to cast it when you use it):
        static void ReaderThread(Object ob)
        { Application.Run(new RForm((FileController)ob)); }

        static void WriterThread(Object ob)
        { Application.Run(new WForm((FileController)ob)); }

        // The self-test builds its own File and controller, so it takes no argument:
        static void AuditThread()
        { Application.Run(new AuditForm()); }
    }
}
