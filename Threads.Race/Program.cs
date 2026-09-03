using System;
using System.Threading;

namespace Threads.Race
{
    // An object that holds a fixed supply of "tokens" to give away.
    //
    // ============================ YOUR TASK ============================
    //
    // Run this program. It hands out MORE tokens than it ever had.
    //
    // Why? Because getToken() does its work in three separate steps:
    //
    //     if (tokens > 0)              // (*) read the value
    //     {
    //         tokens = tokens - 1;     // (**) read it again, subtract, write it back
    //         outcome = true;
    //     }
    //
    // The processor may pause a thread at (*) or at (**). A thread paused at (*)
    // has already decided a token is available -- but another thread can take that
    // very token before the paused thread wakes up. A thread paused inside (**) will
    // wake up and write back a value computed from a reading that is now stale,
    // erasing whatever the other threads did in the meantime.
    //
    // TODO: Make getToken() atomic, so that the supply can never go below zero and
    //       the total handed out can never exceed the starting supply. Use the C#
    //       lock statement on a private object that this class owns.
    //
    // Do NOT change SUPPLY, THREADS, or ROUNDS to hide the problem. Fix the class.
    // ===================================================================
    class TokenObject
    {
        private int tokens;

        // TODO: declare your private lock object here.

        public TokenObject(int supply) { tokens = supply; }

        // Returns true if a token was available and has been seized.
        public bool getToken()
        {
            bool outcome = false;
            if (tokens > 0)
            {
                tokens = tokens - 1;
                outcome = true;
            }
            return outcome;
        }

        // How many tokens remain. Should never be negative.
        public int Remaining { get { return tokens; } }
    }

    class Program
    {
        const int SUPPLY = 20000;   // tokens available at the start of each round
        const int THREADS = 4;      // threads competing for them
        const int ROUNDS = 20;      // how many times we repeat the experiment

        static TokenObject x;
        static int[] seized;

        // The starting gate. Threads are created, then held here until every one of
        // them is ready, so that they all begin competing at the same instant.
        //
        // Without this, the first thread can drain the whole supply before the last
        // thread has even started running -- starting a thread costs far more time
        // than the loop below takes -- and then there is no contention to observe.
        static ManualResetEventSlim gate;
        static int ready;

        static void Main(string[] args)
        {
            WidenConsole();

            Console.WriteLine("Token race: {0} threads competing for {1:N0} tokens, {2} rounds.",
                              THREADS, SUPPLY, ROUNDS);
            Console.WriteLine("A correct TokenObject hands out exactly {0:N0} tokens every round.", SUPPLY);
            Console.WriteLine();
            Console.WriteLine("            handed out       expected   overdraw    left");
            Console.WriteLine("            ----------       --------   --------    ----");

            int failedRounds = 0;
            int worstOverdraw = 0;

            for (int round = 1; round <= ROUNDS; round++)
            {
                x = new TokenObject(SUPPLY);
                seized = new int[THREADS];
                gate = new ManualResetEventSlim(false);
                ready = 0;

                Thread[] runners = new Thread[THREADS];
                for (int t = 0; t < THREADS; t++)
                {
                    int id = t;                       // capture a copy for this thread
                    runners[t] = new Thread(delegate() { loop(id); });
                }

                for (int t = 0; t < THREADS; t++) { runners[t].Start(); }

                // Wait until every thread has arrived at the gate, then release them:
                while (Volatile.Read(ref ready) < THREADS) { Thread.Sleep(1); }
                gate.Set();

                for (int t = 0; t < THREADS; t++) { runners[t].Join(); }
                gate.Dispose();

                int total = 0;
                for (int t = 0; t < THREADS; t++) { total += seized[t]; }

                int overdraw = total - SUPPLY;
                bool ok = (overdraw == 0 && x.Remaining == 0);
                if (!ok)
                {
                    failedRounds++;
                    if (overdraw > worstOverdraw) { worstOverdraw = overdraw; }
                }

                Console.WriteLine("round {0,2}:  {1,10:N0}  of  {2,8:N0}   {3,8:N0}   {4,5:N0}   {5}",
                                  round, total, SUPPLY, overdraw, x.Remaining, ok ? "ok" : "BROKEN");
            }

            Console.WriteLine();
            Console.WriteLine("'overdraw' is how many tokens were handed out beyond the supply.");
            Console.WriteLine("'left' is what the counter finished at; it should never go below 0.");
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine("Rounds broken   : {0} of {1}", failedRounds, ROUNDS);
            Console.WriteLine("Worst overdraw  : {0:N0} extra tokens", worstOverdraw);
            Console.WriteLine("Verdict         : {0}", failedRounds == 0 ? "PASS" : "FAIL");
            if (failedRounds == 0)
            {
                Console.WriteLine();
                Console.WriteLine("The supply held. Run it a few more times to be sure.");
            }
            Console.WriteLine("=================================================================");
            Console.WriteLine();
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        // Gives the console a wider buffer so the report does not wrap, and enough
        // scrollback to hold every round. Harmless if the environment refuses.
        static void WidenConsole()
        {
            try
            {
                Console.SetBufferSize(120, 1000);
                int w = Math.Min(110, Console.LargestWindowWidth);
                int h = Math.Min(45, Console.LargestWindowHeight);
                Console.SetWindowSize(w, h);
            }
            catch (Exception)
            {
                // Output redirected, or the host will not allow resizing. Not a problem.
            }
        }

        // Repeatedly seizes tokens until the supply is exhausted.
        // param: id - the index number of the thread that called this procedure.
        static void loop(int id)
        {
            Interlocked.Increment(ref ready);   // report for duty
            gate.Wait();                        // and wait for the starting signal

            int success = 0;
            while (x.getToken()) { success = success + 1; }
            seized[id] = success;
        }
    }
}
