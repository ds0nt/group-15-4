using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StoreSim
{
    class Customer : iSPSObserver
    {
        //Shopping Item Lists
        public List<Item> itemList;
        public List<Item> shoppingCart;
        public List<Item> purchasedItems;

        private ServicePoint _goToSP;

        //Customer Position In Store, Currently Unimplemented
        public double X { get; set; }
        public double Y { get; set; }

        //Customer ID, Globally unique

        private static int _lastId = 1;
        private int _id;
        public int ID { get { return _id; }}

        private int _delay = 0;

        //Super Rhudimentary State Machine
        enum CustomerState
        {
            Shopping, // buying items
            MainQueue, // waiting to get into service point queue
            FrontOfMainQueue, // trying to select SPQueue
            ServicePointQueue, // waiting for people in front of him
            Exiting, //scanning items
        };
        public struct CustomerStart
        {
            public int items;
            public int delay;
        };
        CustomerState state;

        public Customer(CustomerStart c)
        {
            state = CustomerState.Shopping;
            _id = _lastId;
            _lastId++;
            
            itemList = Item.GenerateRandomItems(c.items);

            shoppingCart = new List<Item>();
            purchasedItems = new List<Item>();

            _delay = c.delay;
            new Thread(new ThreadStart(this.Begin)).Start();
        }

        //main function of the customer
        public void Begin()
        {
            Thread.Sleep((int)(_delay / Store.Get().StoreParams.TimeScale));
            while (Store.Get().open == false)
            {
                System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer * 5000);
            }
            Program.Debug("Customer #" + ID + " (wants " + itemList.Count + " items) -> Browsing Store");
            List<Customer> cp = Store.Get().CustomerPool;
            lock (cp)
            {
                if (cp.Contains(this) == false)
                    cp.Add(this);
                Program.Debug("customer in store count = " + cp.Count);
            }
            while (true)
            {
                System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer * 1000);
                if (!ProcessSelf())
                    return;
            }
        }

        //The Brains of the Customer. The Ugliness is Densest Here.
        public bool ProcessSelf()
        {
            switch (state)
            {
                case CustomerState.Shopping:
                    //Wait for Finding Item
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.TimeToBrowsePerItem);
                    
                    //Find Random Item on the List and put in in the cart!
                    lock (this)
                    {
                        Item found = itemList[Store.rand.Next(0, itemList.Count)];
                        itemList.RemoveAt(Store.rand.Next(0, itemList.Count));
                        shoppingCart.Add(found);
                    }
                    if (itemList.Count == 0) // No More items on our List so go to queue
                    {
                        //MainQueue State change
                        state = CustomerState.MainQueue;
                        Queue<Customer> l = Store.Get().MainQueue;
                        lock (l)
                        {
                            l.Enqueue(this);
                            Program.Debug("Customer #" + ID + " -> Main Queue in Pos " + l.Count);
                        }
                    }
                    break;
                case CustomerState.MainQueue:
                    //Just wait until we are at the front of the Main Queue
                    if (Store.Get().MainQueue.Peek() == this)
                    {
                        //We are first now Change States
                        state = CustomerState.FrontOfMainQueue;
                        // -- Start observing the Service Points
                        Store.Get().SPS.RegisterObserver(this);
                        //Check if we can just go
                        OnSPSUpdate();
                    }
                    break;
                case CustomerState.FrontOfMainQueue:
                    // _goToSP will be set in a different thread via OnSPSUpdate()
                    //Check if we have been notified and want to go somewhere
                    if (!upToDate)
                        tryMove();
                    if (_goToSP != null)
                    {
                        lock (this)
                        {
                            if (_goToSP.EnqueueCustomer(this)) //Another customer might have ninja'd it
                            {
                                //Change to SP Queue
                                lock (Store.Get().MainQueue)
                                {
                                    Store.Get().MainQueue.Dequeue();
                                    Store.Get().SPS.UnregisterObserver(this);
                                }
                                state = CustomerState.ServicePointQueue;
                            }
                            _goToSP = null;
                        }
                    }
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer);
                    break;

                case CustomerState.ServicePointQueue:
                    //Waiting in Line... Waiting in Line... Items Scanning... ladedaa.
                    //This will be broken by the ServicePoint Telling us to get out of the line
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer);
                    break;

                case CustomerState.Exiting:
                    //Bye Bye Store
                    Program.Debug("Customer #" + ID + " -> Finished Paying");
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.TimeToExitStore);
                    lock (Store.Get().CustomerPool)
                    {
                        Store.Get().CustomerPool.Remove(this);
                    }
                    Program.Debug("Customer #" + ID + " -> Exited Store");
                    Program.Debug("customer in store count = " + Store.Get().CustomerPool.Count);
                    return false;
             }
            return true;
        }

        //Calculates the distance from the Customer to SP. used for SP Priority
        private double _distanceToSP(ServicePoint s)
        {
            double dx = s.X - X;
            double dy = s.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        //Logic to go to our favorite SP given free SPs
        private ServicePoint _selectFavoriteSP(List<ServicePoint> sp)
        {
            ServicePoint favorite = null;
            int leastItems = int.MaxValue;
            double leastDistance = int.MaxValue;
            foreach (ServicePoint s in sp)
            {
                int items = s.GetQueuedItems();
                if (items < leastItems || (items == leastItems))
                {
                    //if we have a tie for item count, choose based on distance
                    double distance = _distanceToSP(s);
                    if (items == leastItems)
                    {
                        if (distance >= leastDistance)
                            continue;
                    }
                    leastItems = items;
                    leastDistance = distance;
                    favorite = s;
                }
            }
            return favorite;
        }
        bool upToDate = true;
        //Observer Pattern, Watches SPS
        public void OnSPSUpdate()
        {
            upToDate = false;
        }

        public void tryMove()
        {
            upToDate = true;
            List<ServicePoint> sp = Store.Get().SPS.GetAvailableSP();
            ServicePoint s = _selectFavoriteSP(sp);
            if (s != null)
            {
                lock (this)
                {
                    _goToSP = s;
                }
            }
        }

        //Service Point calls this to kick us out of line because were done paying
        public void LetExit()
        {
            lock (this)
            {
                state = CustomerState.Exiting;
            }
        }

        public static CustomerStart CreateFromSim(string l)
        {
            string itemstr = l.Substring(0, l.IndexOf(';'));
            string delaystr = l.Substring(l.IndexOf(';')+1);

            CustomerStart c;
            c.items = int.Parse(itemstr);
            c.delay = int.Parse(delaystr);
            return c;
        }

        public static void resetCounter()
        {
            _lastId = 0;
        }
    }
}
