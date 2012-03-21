using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class ServicePoint
    {
        private Queue<Customer> _queue;
        private List<iSPObserver> _observers;
        public double X { get; set; }
        public double Y { get; set; }
        

        public ServicePoint()
        {
            _observers = new List<iSPObserver>();
            _queue = new Queue<Customer>();
        }

        //Start my Thread
        public void Begin()
        {

        }
        
        //count items in this queue, careful not to deadlock with customers calling this
        public int GetQueuedItems()
        {
            int count = 0;
            foreach(Customer c in _queue)
            {
                count += c.shoppingCart.Count;
            }
            return count;
        }

        //Observer pattern add observer
        public void RegisterObserver(iSPObserver obs)
        {
            _queue = new Queue<Customer>();
            _observers.Add(obs);
        }

        //Observer Pattern Remove Observer
        public void UnregisterObserver(iSPObserver obs)
        {
            _observers.Remove(obs);
        }

        //Queue full?
        public bool IsFull()
        {
            return _queue.Count >= Store.Get().StoreParams.QueueMaxSize;
        }
    }
}