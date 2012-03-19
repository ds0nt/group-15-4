﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StoreSim
{
    class Store
    {
        #region Variables
        private static Store myInstance;
        
        private StoreParams _storeParams;
        private ServicePointSystem _spm;
        private Queue<Customer> _mainQueue;
        private Manager _manager;


        public Manager Manager
        { 
            get { return _manager; }
        }
        public StoreParams StoreParams 
        { 
            get { return _storeParams; }
        }

        public ServicePointSystem SPS
        {
            get { return _spm; }
        }

        public Queue<Customer> MainQueue
        {
            get { return _mainQueue; }
        }

        #endregion
        #region Constructor
        public Store(StoreParams sp)
        {
            Program.debug("Creating Store...");
            //Store params are the C-01 - C-12 variables
            _storeParams = sp;
            //Singleton Instance
            myInstance = this;

            //Has references to SP and SPQueues
            _spm = new ServicePointSystem();

            //Main Customer Queue
            _mainQueue = new Queue<Customer>();

            //Manager
            _manager = new Manager();
        }

        //Gets Singleton Instance of this class
        public static Store get()
        {
            if (myInstance == null)
                throw new NullReferenceException();
                
            return myInstance;
        }
        #endregion

        //Simulates the store over deltaTime
        public void Simulate(double deltaTimeMS)
        {
            //Create customers based on probability over time

            new Thread(new ThreadStart(new Customer().begin)).Start();
        }
    }
}
