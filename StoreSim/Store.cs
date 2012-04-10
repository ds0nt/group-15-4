using System;
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
        public bool open { get; set; }
        private StoreParams _storeParams;
        private ServicePointSystem _spm;
        private Queue<Customer> _mainQueue;
        private List<Customer> _customerPool; ///list of customers 
        private Manager _manager;
        public static Random rand = new Random();

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
        public List<Customer> CustomerPool
        {
            get { return _customerPool; }
        }

        #endregion 
        #region Constructor
        public Store(StoreParams sp)
        {
            Program.Debug("Creating Store...");
            //Store params are the C-01 - C-12 variables
            _storeParams = sp;

            open = false;
            //Singleton Instance
            myInstance = this;

            //Has references to SP and SPQueues
            ServicePoint.resetCounter();
            _spm = new ServicePointSystem();

            //Main Customer Queue
            Customer.resetCounter();
            _mainQueue = new Queue<Customer>();

            //customer pool
            _customerPool = new List<Customer>();

            //Manager
            _manager = new Manager();
        }

        //Gets Singleton Instance of this class
        public static Store Get()
        {
            if (myInstance == null)
                throw new NullReferenceException();
                
            return myInstance;
        }
        #endregion

        /*
        public int GetNumberOfCustomer()
        {
            return CustomerPool.Count;
        }*/

        //Simulates the store over deltaTime
        public void Simulate(double deltaTimeMS)
        {
            //double next = rand.NextDouble();
            //if (next < (_storeParams.RandomCustomerGenRate * deltaTimeMS))
            //{
            //    new Customer();
            //}
        }
    }
}