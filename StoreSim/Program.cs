using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
namespace StoreSim
{
    static class Program
    {
        static bool debugEnabled = true;
        static DateTime BeginTime = DateTime.Now;
        public static Exception lastException = null;
        static StringBuilder log = new StringBuilder();

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
                string line = delta.ToString() + " -- " + s;
                Console.WriteLine(line);
                log.AppendLine(line);
            }
            catch (NullReferenceException) // Store Not yet Created
            {
                Console.WriteLine(s);
            }
        }

        public static bool saveLog(Stream stream)
        {
            try
            {
                StreamWriter f = new StreamWriter(stream);
                f.Write(log.ToString());
                f.Flush();
                log = new StringBuilder();
                return true;
            }
            catch (Exception e)
            {
                lastException = e;
                log = new StringBuilder();
                return false;
            }
        }

        public static Customer.CustomerStart[] readSimulation(Stream stream)
        {
            List<Customer.CustomerStart> cs = new List<Customer.CustomerStart>();
            try
            {
                StreamReader f = new StreamReader(stream);
                while (!f.EndOfStream)
                {
                    string l = f.ReadLine();
                    cs.Add(Customer.CreateFromSim(l));
                }
                return cs.ToArray();
            }
            catch (Exception e)
            {
                lastException = e;
                return null;
            }
        }
        public static StoreParams readSettings(Stream stream)
        {
            try
            {
                StreamReader f = new StreamReader(stream);
                string settings = f.ReadToEnd();
                f.Close();
                StoreParams sp = StoreParams.createFromSettings(settings);
                return sp;    
            }
            catch (Exception e)
            {
                lastException = e;
                return null;
            }
        }

        public static void randomSimulation(StoreParams sp)
        {

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

        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new GUI.StoreForm());
        }


        public static bool writeSettings(StoreParams sp, Stream stream)
        {
            try
            {
                StreamWriter f = new StreamWriter(stream);
                foreach (System.Reflection.PropertyInfo pi in sp.GetType().GetProperties())
                {
                    f.Write(pi.Name + " = " + pi.GetValue(sp, null) + ";\n\r");
                }
                f.Flush();
                return true;
            }
            catch (Exception e)
            {
                lastException = e;
                return false;
            }
        }

        public static bool writeSim(Customer.CustomerStart[] simCustomers, Stream stream)
        {
            try
            {
                StreamWriter f = new StreamWriter(stream);
                foreach (Customer.CustomerStart cs in simCustomers)
                {
                    f.WriteLine(cs.items + ";" + cs.delay);
                }
                f.Flush();
                return true;
            }
            catch (Exception e)
            {
                lastException = e;
                return false;
            }
        }
    }
}
