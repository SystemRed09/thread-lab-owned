using System.Collections.Generic;

namespace Threads.Core
{
    // Models a simple text file that can be read or written one line at a time.
    // It implements both interfaces Reader and Writer, depending on how it is used.
    //
    // NOTE: This class is deliberately NOT thread-safe. Protecting it is the job of
    // FileController, which is the only class that hands out Reader/Writer handles.
    //
    // Note also where 'count' lives. It is the reader's cursor AND the writer's line
    // counter, kept on the file itself. That only works while at most one session
    // exists at a time -- which is the guarantee FileController is supposed to make.
    // If this program were ever extended to allow several readers at once, they
    // would consume each other's lines, and no amount of locking would help: nothing
    // would be racing. The cursor would have to move off the File and onto whatever
    // openRead() returns.
    public class File : Reader, Writer
    {
        private List<string> lines;  // the "file" is a list of strings
        private int count;           // counts the lines read/written so far

        public Status status = Status.Closed;

        public File() { lines = new List<string>(); }

        // initializes the file for reading its lines one by one:
        public void initRead() { count = 0; }

        // initializes the file for writing fresh lines to it. Erases existing contents.
        public void initWrite() { lines = new List<string>(); count = 0; }

        // reads and returns the next line of the file (or returns null if no more lines):
        public string readLine()
        {
            string line = null;
            if (count < lines.Count)
            {
                line = lines[count];
                count++;
            }
            return line;
        }

        // writes a new line to the end of the file:
        public void writeLine(string s)
        {
            lines.Add(s);
            count++;
        }
    }
}
