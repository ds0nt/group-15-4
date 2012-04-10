using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StoreSim
{
    class ServicePointSystem : iSPObserver
    {
        private List<ServicePoint> _spList;
        private List<iSPSObserver> _observers;
        //int initCount; //#Andre -- count for open init

        public List<ServicePoint> GetServicePoints()
        {
            return _spList;
        }


        public ServicePointSystem()
        {
            _observers = new List<iSPSObserver>();
            _spList = new List<ServicePoint>();

            for (int i = 0; i < Store.Get().StoreParams.MaximumServicePoints; i++) //#ANDRE
            {
                ServicePoint s = new ServicePoint();
                _spList.Add(s);
                s.RegisterObserver(this);
                //s.Start();
            }
            for (int i = 0; i < Store.Get().StoreParams.InitialServicePoints; i++) //#Andre
            {
                _spList.ElementAt(i).Start();
                
            }
            NotifyObservers();
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

        public void AddServicePoint()
        {
            ServicePoint s = new ServicePoint();
            _spList.Add(s);
            s.RegisterObserver(this);
            s.Start();
            NotifyObservers();
        }
        public void OpenServicePoint(ServicePoint p)
        {
            p.Start();
            NotifyObservers();
        }

        public void CloseServicePoint(ServicePoint p)
        {
            p.Close();
            //_spList.Remove(p);
            NotifyObservers();
        }

        //Observer Pattern Notify Observers (Customers)
        private void NotifyObservers()
        {
            lock (_observers)
            {
                foreach (iSPSObserver obs in _observers)
                    if(obs != null)
                        obs.OnSPSUpdate();
            }
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

        public List<ServicePoint> GetOpenedSP()
        {
            List<ServicePoint> a = new List<ServicePoint>();
            lock (_spList)
            {
                foreach (ServicePoint s in _spList)
                {
                    if (s.Opened == true)
                    {
                        a.Add(s);
                    }
                }
            }
            return a;
        }
    }
}
