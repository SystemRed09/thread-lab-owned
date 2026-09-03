using System;
using System.Text;
using System.Threading;

namespace Threads.Core
{
    // Result of one self-test run. See SelfTest below for what these numbers mean.
    public class SelfTestResult
    {
        public int Opens;       // handles successfully obtained
        public int Refusals;    // times the controller correctly said "busy, try later"
        public int Violations;  // times two threads held the file AT THE SAME MOMENT
        public int Errors;      // exceptions thrown from inside File (corrupted state)

        // If nobody was ever let in, the run proves nothing: zero threads holding the
        // file cannot produce two threads holding the file. A "pass" on no data is
        // not a pass.
        public bool Inconclusive { get { return Opens == 0; } }

        public bool Passed { get { return !Inconclusive && Violations == 0 && Errors == 0; } }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Handles granted : " + Opens + "   (times a thread was let in)");
            sb.AppendLine("Refused (busy)  : " + Refusals + "   (times it was correctly told to wait)");
            sb.AppendLine("VIOLATIONS      : " + Violations + "   (times TWO threads held the file at once)");
            sb.AppendLine("Errors in File  : " + Errors + "   (crashes from concurrent use of the List)");
            sb.AppendLine();

            if (Inconclusive)
            {
                sb.AppendLine("INCONCLUSIVE - nobody was ever let in.");
                sb.AppendLine();
                sb.AppendLine("Every attempt was refused, so no two threads could possibly have");
                sb.AppendLine("overlapped. This run proves nothing either way.");
                sb.AppendLine();
                sb.AppendLine("Check that close() returns the state to Closed.");
            }
            else if (Passed)
            {
                sb.AppendLine("PASS - no two threads ever held the file at once.");
                if (Refusals == 0)
                {
                    sb.AppendLine();
                    sb.AppendLine("WARNING: the controller never refused anyone. Check that");
                    sb.AppendLine("openRead/openWrite really return null when the file is busy.");
                }
            }
            else
            {
                sb.AppendLine("FAIL - the file was held by more than one thread at a time.");
                sb.AppendLine("Fix FileController and run this test again.");
            }
            return sb.ToString();
        }
    }

    // WHY THIS EXISTS
    //
    // FileController has two defects, and they hide very differently.
    //
    // The FIRST one you can expose by hand, slowly, on purpose: open the writer,
    // then open a reader that should have been refused, and watch the two sessions
    // trample each other's position in the file. No timing luck required.
    //
    // The SECOND one survives the fix for the first. Once a state variable exists
    // but is not guarded, two threads can both read "Closed", both conclude the file
    // is free, and both open it. That window is a few instructions wide. You will
    // never click your way into it, and the program will pass every hand test you
    // can perform while remaining wrong.
    //
    // HOW IT DETECTS A VIOLATION
    //
    // Every thread that receives a handle increments a shared counter, and
    // decrements it when it lets go. If a thread ever pushes that counter from 1 to
    // 2, then two threads were holding the file at once, and the controller failed
    // at its one job. That is a VIOLATION.
    //
    // (The counter is updated with Interlocked so that the detector itself cannot
    // suffer the very race it is looking for.)
    //
    // The number it reports is your finish line. Fix FileController until VIOLATIONS
    // reads 0, several runs in a row.
    //
    // This class is TEST SCAFFOLDING. You do not need to modify it, you do not need
    // to understand every line of it, and it does NOT belong in your class diagram.
    public static class SelfTest
    {
        // Runs the test on its OWN File and FileController, so that whatever the
        // Reader and Writer forms happen to be doing cannot interfere with it.
        //
        // threadCount threads each perform iterations open/close cycles.
        public static SelfTestResult Run(int threadCount, int iterations)
        {
            FileController c = new FileController(new File());
            SelfTestResult result = new SelfTestResult();

            int opens = 0, refusals = 0, violations = 0, errors = 0;
            int active = 0;   // how many threads believe they currently hold the file
            int ready = 0;    // how many threads have reached the starting gate

            // Threads are held here until all of them exist. Otherwise the first
            // thread can finish its whole run before the last one starts, and there
            // is no contention to observe.
            ManualResetEventSlim gate = new ManualResetEventSlim(false);

            Thread[] workers = new Thread[threadCount];

            for (int t = 0; t < threadCount; t++)
            {
                int id = t;
                workers[t] = new Thread(delegate()
                {
                    Random rand = new Random(id * 7919 + 13);

                    Interlocked.Increment(ref ready);
                    gate.Wait();

                    for (int i = 0; i < iterations; i++)
                    {
                        bool wantsToRead = (rand.Next(2) == 0);
                        object handle = null;

                        try
                        {
                            handle = wantsToRead ? (object)c.openRead() : (object)c.openWrite();
                        }
                        catch (Exception)
                        {
                            Interlocked.Increment(ref errors);
                            continue;
                        }

                        if (handle == null)
                        {
                            Interlocked.Increment(ref refusals);
                            continue;
                        }

                        Interlocked.Increment(ref opens);

                        // We are holding the file. If anyone else is too, that is a violation.
                        if (Interlocked.Increment(ref active) > 1)
                        {
                            Interlocked.Increment(ref violations);
                        }

                        try
                        {
                            // Do a little work through the handle, and stay on the
                            // processor while doing it, so that a thread which raced
                            // us into the file is still here to be noticed.
                            if (wantsToRead)
                            {
                                ((Reader)handle).readLine();
                            }
                            else
                            {
                                ((Writer)handle).writeLine("thread " + id + " line " + i);
                            }
                            Thread.SpinWait(60);
                        }
                        catch (Exception)
                        {
                            // File is not thread-safe; concurrent use can corrupt it outright.
                            Interlocked.Increment(ref errors);
                        }

                        Interlocked.Decrement(ref active);
                        c.close();
                    }
                });
            }

            for (int t = 0; t < threadCount; t++) { workers[t].Start(); }

            while (Volatile.Read(ref ready) < threadCount) { Thread.Sleep(1); }
            gate.Set();

            for (int t = 0; t < threadCount; t++) { workers[t].Join(); }
            gate.Dispose();

            result.Opens = opens;
            result.Refusals = refusals;
            result.Violations = violations;
            result.Errors = errors;
            return result;
        }
    }
}
