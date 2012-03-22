using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    struct StoreParams
    {
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
    }
}
