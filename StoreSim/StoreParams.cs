using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class StoreParams
    {
        public bool ManagerPresent { get; set; }
       

        public int MaximumServicePoints { get; set; } //C-01
        public int MinimumServicePoints { get; set; } //C-04
        public int InitialServicePoints { get; set; } //C-11
        public int QueueMaxSize { get; set; } //C-12

        public float RandomCustomerGenRate { get; set; }

        public float TimeScale { get; set; }

        private int _timeToScan;
        public int TimeToScan 
        { 
            get { return (int)(_timeToScan / TimeScale); } 
            set {_timeToScan = value; } 
        }

        private int _timeToPurchase;
        public int TimeToPurchase 
        {
            get { return (int)(_timeToPurchase / TimeScale); }
            set { _timeToPurchase = value; } 
        }

        private int _timeToExitStore;
        public int TimeToExitStore
        {
            get { return (int)(_timeToExitStore / TimeScale); }
            set { _timeToExitStore = value; }
        }

        private int _reactionTimeSP;
        public int ReactionTimeSP 
        {
            get { return (int)(_reactionTimeSP / TimeScale); }
            set { _reactionTimeSP = value; } 
        }
        private int _timeToBrowsePerItem;
        public int TimeToBrowsePerItem 
        {
            get { return (int)(_timeToBrowsePerItem / TimeScale); }
            set { _timeToBrowsePerItem = value; } 
        }
        
        private int _reactionTimeCustomer;
        public int ReactionTimeCustomer {
            get { return (int)(_reactionTimeCustomer / TimeScale); }
            set { _reactionTimeCustomer = value; } 
        }

        public bool RandomItemGeneration { get; set; }

        public int RandomItemMin { get; set; }

        public int RandomItemMax { get; set; }

        public void reflectionSet(string name, string value)
        {
            int intval;
            float floatval;
            if (value.ToLower().Equals("true"))
                this.GetType().GetProperty(name).SetValue(this, true, null);
            else if (value.ToLower().Equals("false"))
                this.GetType().GetProperty(name).SetValue(this, false, null);
            else if (int.TryParse(value, out intval))
                this.GetType().GetProperty(name).SetValue(this, intval, null);
            else if (float.TryParse(value, out floatval))
                this.GetType().GetProperty(name).SetValue(this, floatval, null);
            else
                this.GetType().GetProperty(name).SetValue(this, value, null);
        }

        public static StoreParams createFromSettings(string settings)
        {
            //do awesome things with reflection
            StoreParams sp = new StoreParams();
            string[] clauses = settings.Replace("\n", "").Replace(" ", "").Replace("\r", "").Split(';');
            foreach (string clause in clauses)
            {
                if (clause.Equals(""))
                    continue;
                string key = clause.Substring(0, clause.IndexOf("="));
                string value = clause.Substring(clause.IndexOf("=") + 1);
                if (key.Equals("") || value.Equals(""))
                    continue;
                sp.reflectionSet(key, value);
            }
            return sp;
        }

        public bool ManagerCanBreak { get; set; }


        private int _managerBreakTime;
        public int ManagerBreakTime
        {
            get { return (int)(_managerBreakTime / TimeScale); }
            set { _managerBreakTime = value; }
        }
    }
}
