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

        //Observer pattern add observer
        public void RegisterObserver(iSPObserver obs)
        {
            _observers.Add(obs);
        }

        //Observer Pattern Remove Observer
        public void UnregisterObserver(iSPObserver obs)
        {
            _observers.Remove(obs);
        }

        public ServicePoint()
        {
            _observers = new List<iSPObserver>();
        }

        public void Begin()
        {

        }
    }
}