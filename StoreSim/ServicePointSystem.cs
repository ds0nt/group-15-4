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
            for (int i = 0; i < Store.get().StoreParams.InitialServicePoints; i++)
            {

            }
        }

        public void RegisterObserver(iSPSObserver observer)
        {
            _observers.Add(observer);
        }

        public void UnregisterObserver(iSPSObserver observer)
        {
            _observers.Remove(observer);
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
        public void onSPUpdate()
        {
            NotifyObservers();
        }

        public ServicePoint getAvailableSP()
        {
            ServicePoint a = new ServicePoint();
            return a;
        }
    }
}
