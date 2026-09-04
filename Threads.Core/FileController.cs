namespace Threads.Core
{
    // A controller for a sequential text file. It is the single point of access to
    // the File: any thread that wants to read or write must ask this controller
    // for a handle first, and must call close() when it is done.
    //
    // As shipped, this controller enforces NOTHING. It will happily hand a Reader
    // to one thread while another thread is in the middle of writing. Your job is
    // to make it correct.
    //
    // ============================ YOUR TASK ============================
    //
    // TODO 1 - EXCLUSIVE ACCESS
    //   Add a state variable of type Status that remembers whether the file is
    //   currently Closed, being read, or being written. openRead() and openWrite()
    //   must return null when the file is already in use by someone else, and
    //   close() must return the file to the Closed state.
    //
    // TODO 2 - ATOMIC TEST-AND-SET
    //   TODO 1 alone is not enough. Two threads can both read the state, both see
    //   Closed, and both decide to open -- because "check the state, then change
    //   the state" is two separate steps and the processor may pause a thread
    //   between them. Use a lock so that checking and changing the state happen as
    //   one indivisible step.
    //
    //   Lock on a private object that you own, NOT on `this`. Any code holding a
    //   reference to this controller can lock `this` and stall you; nobody outside
    //   this class can reach a private field.
    //
    // Run Threads.ReadWrite and press "Run Self-Test" to check your work.
    // ===================================================================

    public class FileController
    {
        private File theFile;  // the file controlled by this controller

        // TODO 1: declare your Status state variable here.

        // TODO 2: declare your private lock object here.

        public FileController(File f) { theFile = f; }

        private static readonly object Gate = new object();

        // Opens the file for reading and returns a handle to it.
        // Returns null if the file cannot be opened right now.
        public Reader openRead() {
            lock (Gate) {
                object Gate = new object();
                if (theFile.status != Status.Closed) { return null; }

                Reader r = null;
                theFile.initRead();
                theFile.status = Status.Reading;
                r = theFile;
            return r;
            }
        }

        // Opens the file for writing and returns a handle to it.
        // Returns null if the file cannot be opened right now.
        public Writer openWrite() {
            lock (Gate) {
                if (theFile.status != Status.Closed) { return null; }
                Writer w = theFile;
                theFile.initWrite();
                theFile.status = Status.Writing;
                w = theFile;
                return w;
            }
        }

        // Releases the file so that another thread may open it.
        public void close()
        {
            theFile.status = Status.Closed;
        }
    }
}
