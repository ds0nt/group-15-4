using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class ServicePointSystem : iSPObserver
    {
        private List<ServicePoint> _spList;
        private List<iSPSObserver> _observers;

        public ServicePointSystem()
        {
            for (int i = 0; i < Store.Get().StoreParams.InitialServicePoints; i++)
            {

            }
        }

        public void RegisterObserver(iSPSObserver observer)
        {
            lock (_observers)
            {
                _observers.Add(observer);
            }
        }

        public void UnregisterObserver(iSPSObserver observer)
        {
            lock (_observers)
            {
                _observers.Remove(observer);
            }
        }

        public void AddServicePoint(ServicePoint c)
        {
            _spList.Add(c);
            NotifyObservers();
        }

        public void CloseServicePoint(ServicePoint p)
        {
            _spList.Remove(p);
            NotifyObservers();
        }

        //Observer Pattern Notify Observers (Customers)
        private void NotifyObservers()
        {
            foreach (iSPSObserver obs in _observers)
                obs.onSPSUpdate();
        }

        //When a Service Point Updates
        public void OnSPUpdate()
        {
            NotifyObservers();
        }

        public List<ServicePoint> GetAvailableSP()
        {
            List<ServicePoint> a = new List<ServicePoint>();
            lock (_spList)
            {
                foreach (ServicePoint s in _spList)
                {
                    if(!s.IsFull())
                        a.Add(s);
                }
            }
            return a;
        }
    }
}
