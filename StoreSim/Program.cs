using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    
    class Program
    {

        static bool debugEnabled = true;
        static DateTime BeginTime = DateTime.Now;

        public static void debug(object s)
        {
            if (debugEnabled == false)
                return;
            TimeSpan delta = DateTime.Now - BeginTime;
            Console.WriteLine(
                delta.Hours + ":" +
                delta.Minutes + ":" +
                delta.Seconds + ":" + 
                delta.Milliseconds + " " + s);
        }

        static void Main(string[] args)
        {
            
            StoreParams sp = new StoreParams()
            {
                InitialServicePoints = 2,
                MaximumServicePoints = 2,
                MinimumServicePoints = 2,
                QueueMaxSize = 2
            };

            Store store = new Store(sp);

            DateTime lastTick = DateTime.Now;
            while (true)
            {
                store.Simulate((lastTick - DateTime.Now).TotalMilliseconds);
                lastTick = DateTime.Now;
            }
        }
    }
}
