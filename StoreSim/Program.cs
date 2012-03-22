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


        //Logging Ability
        public static void Debug(object s)
        {
            if (debugEnabled == false)
                return;

            try
            {
                //Calculate Time based on TimeScale (Simulation Speed)
                TimeSpan delta = DateTime.Now - BeginTime;
                delta = new TimeSpan((long)(delta.Ticks * Store.Get().StoreParams.TimeScale));
                Console.WriteLine(
                    delta.Hours + ":" +
                    delta.Minutes + ":" +
                    delta.Seconds + ":" + s);
            }
            catch (NullReferenceException) // Store Not yet Created
            {
                Console.WriteLine(s);
            }
        }

        static void Main(string[] args)
        {
            //Initialize Input Variables
            //Further Documentation in StoreParams Class
            StoreParams sp = new StoreParams()
            {
                InitialServicePoints = 2,
                MaximumServicePoints = 2,
                MinimumServicePoints = 2,
                QueueMaxSize = 2,

                TimeScale = 20,
                TimeToBrowsePerItem = 180000,
                TimeToPurchase = 10000,
                TimeToScan = 2000,
                TimeToExitStore = 1000,
                ReactionTimeCustomer = 100,
                ReactionTimeSP = 100,
                
                RandomCustomerGenRate = 0.001f, //Lower is fewer, 0 is none.
                RandomItemGeneration = false,
                RandomItemMin = 1,
                RandomItemMax = 3
            };

            Store store = new Store(sp);

            DateTime lastTick = DateTime.Now;
            while (true)
            {
                store.Simulate((DateTime.Now - lastTick).TotalMilliseconds);
                lastTick = DateTime.Now;
            
                //sleep it a bit to get a better sample from rand
                System.Threading.Thread.Sleep(10); 
            }
        }
    }
}
