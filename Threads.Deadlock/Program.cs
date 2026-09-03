using System;
using System.Threading;
using Threads.Core;

namespace Threads.Deadlock
{
    // Two jobs, two files, opposite acquisition orders.
    //
    // The Copier reads SOURCE and writes into LOG.
    // The Archiver reads LOG and writes into SOURCE.
    //
    // Each needs both files. Each takes them in the order that reads naturally for
    // its own task. Neither is doing anything obviously wrong -- and yet together
    // they stop the system dead.
    //
    // NOTE: this demo depends on the exclusive access you added to FileController in
    // the Threads.ReadWrite exercise. Finish that one first.
    class Program
    {
        const int WATCHDOG_SECONDS = 5;

        static void Main(string[] args)
        {
            try
            {
                Console.SetBufferSize(120, 1000);
                Console.SetWindowSize(Math.Min(110, Console.LargestWindowWidth),
                                      Math.Min(40, Console.LargestWindowHeight));
            }
            catch (Exception) { }

            FileController source = new FileController(new File());
            FileController log = new FileController(new File());

            FileJob copier = new FileJob("Copier",
                                         source, "SOURCE", Use.Read,
                                         log, "LOG", Use.Write);

            FileJob archiver = new FileJob("Archiver",
                                           log, "LOG", Use.Read,
                                           source, "SOURCE", Use.Write);

            Console.WriteLine("Starting two jobs that each need SOURCE and LOG.");
            Console.WriteLine("Watchdog will call it after {0} seconds.", WATCHDOG_SECONDS);
            Console.WriteLine();

            Thread t1 = new Thread(copier.Run);
            Thread t2 = new Thread(archiver.Run);
            t1.IsBackground = true;
            t2.IsBackground = true;
            t1.Start();
            t2.Start();

            bool done1 = t1.Join(TimeSpan.FromSeconds(WATCHDOG_SECONDS));
            bool done2 = done1 && t2.Join(TimeSpan.FromSeconds(1));

            Console.WriteLine();
            Console.WriteLine("=================================================================");

            if (done1 && done2)
            {
                Console.WriteLine("Both jobs finished. No deadlock observed.");
                Console.WriteLine();
                Console.WriteLine("If you have NOT yet fixed FileController, that is why:");
                Console.WriteLine("without exclusive access the files have no single holder,");
                Console.WriteLine("so nobody has to wait and the circle never closes.");
                Console.WriteLine("Finish Threads.ReadWrite first, then come back here.");
                Console.WriteLine();
                Console.WriteLine("If you HAVE fixed it, run this several more times --");
                Console.WriteLine("then check your fix against the report below.");
            }
            else
            {
                Console.WriteLine("DEADLOCK after {0} seconds. Neither job can proceed.", WATCHDOG_SECONDS);
            }

            Console.WriteLine();
            Report(copier);
            Report(archiver);
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static void Report(FileJob job)
        {
            Console.WriteLine("{0,-9} finished={1,-5} holding={2,-16} waiting for={3}",
                              job.Name, job.Finished, job.Holding, job.WaitingFor);
        }
    }
}
