using System;
using System.Threading;
using Threads.Core;

namespace Threads.Deadlock
{
    // How a job wants to use a file.
    public enum Use { Read, Write }

    // A unit of work that needs TWO files at once: it opens the first, keeps hold of
    // it, and then opens the second. It only releases them once both are finished
    // with -- which is a perfectly ordinary thing for real work to need.
    //
    // ============================ YOUR TASK ============================
    //
    // Run this program. Two jobs each grab one file and then wait forever for the
    // file the other one is holding. Nobody ever gives anything back.
    //
    // Four conditions must ALL hold for a deadlock. Three of them are baked into the
    // design and you cannot remove them here:
    //
    //   1. Mutual exclusion  - the fix you wrote in Threads.Core. A file has one holder.
    //   2. Hold and wait     - a job keeps file one while it waits for file two.
    //   3. No preemption     - nobody can force a job to give a file back.
    //   4. Circular wait     - job A wants what job B holds, and vice versa.
    //
    // Break any ONE of them and the deadlock is gone. Two options:
    //
    //   Option A - IMPOSE A GLOBAL LOCK ORDER (breaks circular wait)
    //     Give every controller a rank, and require every job to acquire files in
    //     increasing rank order, whatever order it happens to want them in.
    //
    //   Option B - ACQUIRE WITH A TIMEOUT AND BACK OFF (breaks hold and wait)
    //     If the second file does not arrive within a deadline, release the first
    //     one, wait a random moment, and start the whole attempt over.
    //
    // TODO: Implement ONE of them. Then write down in REFLECTION.md which you chose,
    //       what it costs, and when the other one would have been the better call.
    //       Neither option is free -- that trade-off is the point of the exercise.
    // ===================================================================
    public class FileJob
    {
        private string name;
        private FileController first;
        private string firstName;
        private Use firstUse;
        private FileController second;
        private string secondName;
        private Use secondUse;

        // Progress, readable from the watchdog thread:
        private volatile string holding = "nothing";
        private volatile string waitingFor = "nothing";
        private volatile bool finished = false;

        public FileJob(string name,
                       FileController first, string firstName, Use firstUse,
                       FileController second, string secondName, Use secondUse)
        {
            this.name = name;
            this.first = first;
            this.firstName = firstName;
            this.firstUse = firstUse;
            this.second = second;
            this.secondName = secondName;
            this.secondUse = secondUse;
        }

        public string Name { get { return name; } }
        public string Holding { get { return holding; } }
        public string WaitingFor { get { return waitingFor; } }
        public bool Finished { get { return finished; } }

        public void Run()
        {
            Log("wants " + firstName + " then " + secondName);

            waitingFor = firstName;
            object h1 = Acquire(first, firstUse);
            holding = firstName;
            waitingFor = "nothing";
            Log("acquired " + firstName);

            // Give the other job time to grab its own first file, so the circle closes.
            Thread.Sleep(200);

            waitingFor = secondName;
            Log("now waiting for " + secondName + " (still holding " + firstName + ")");
            object h2 = Acquire(second, secondUse);
            holding = firstName + " + " + secondName;
            waitingFor = "nothing";
            Log("acquired " + secondName + " -- both files held");

            // The actual work would happen here.
            Thread.Sleep(50);

            second.close();
            first.close();
            holding = "nothing";
            finished = true;
            Log("done, released both");
        }

        // Waits until the controller hands over a handle. Never gives up.
        //
        // If you choose Option B you will want a variant of this that gives up after
        // a deadline and reports failure instead of waiting forever.
        private object Acquire(FileController c, Use use)
        {
            for (int i=0; i < 10; i++) {
                object handle = (use == Use.Read) ? (object)c.openRead() : (object)c.openWrite();
                if (handle != null) { return handle; }
                Thread.Sleep(5);   // busy; try again shortly
            }
            return null;
        }

        private void Log(string message)
        {
            Console.WriteLine("[{0,-9}] {1}", name, message);
        }
    }
}
