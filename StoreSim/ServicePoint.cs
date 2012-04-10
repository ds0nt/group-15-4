using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace StoreSim
{
    class ServicePoint
    {
        private Queue<Customer> _queue;
        private List<iSPObserver> _observers;
        private static int _lastID = 1;
        public int ID;
        public double X { get; set; }
        public double Y { get; set; }
        private bool _opened;
        public bool Opened { get { return _opened; } }

        public ServicePoint()
        {
            _opened = false;
            ID = _lastID;
            _lastID++;

            X = Store.rand.Next();
            Y = Store.rand.Next();
            
            _observers = new List<iSPObserver>();
            _queue = new Queue<Customer>();


            Program.Debug("Service Point #" + ID + " -> Created, Opened: " + _opened);  //#Andre -- The service point is created!! It's closed

        }

        //Thread Begin
        public void Start()
        {
            new Thread(new ThreadStart(this.Open)).Start();
        }
        
        //Thread Loop
        public void Open()
        {
            Program.Debug("Service Point #" + ID + " -> Opened");
            _opened = true;

            Customer servee = null;
            while (_opened || _queue.Count > 0)
            {
                if(_queue.Count > 0)
                    servee = _queue.Peek();
                while(servee != null)
                {
                    while (servee.shoppingCart.Count > 0)
                    {
                        System.Threading.Thread.Sleep(Store.Get().StoreParams.TimeToScan);
                        lock (servee)
                        {
                            servee.purchasedItems.Add(servee.shoppingCart[0]);
                            servee.shoppingCart.RemoveAt(0);
                        }
                    }
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.TimeToPurchase);
                    _queue.Dequeue().LetExit();
                    _notify();
                    servee = null;
                }
                System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeSP);
            }
        }

        public void Close()
        {
            _opened = false;

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

        private void _notify()
        {
            foreach (iSPObserver obs in _observers)
            {
                obs.OnSPUpdate();
            }
        }

        public bool EnqueueCustomer(Customer c)
        {
            if (_opened && !IsFull())
            {
                lock (this)
                {
                    _queue.Enqueue(c);
                    Program.Debug("Customer #" + c.ID + " -> SP #" + ID);
                }
                _notify();
                return true;
            }
            return false;
        }

        public int GetNumberOfCustomers()
        {
            return _queue.Count;
        }
    }
}