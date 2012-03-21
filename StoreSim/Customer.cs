using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Customer : iSPSObserver
    {
        public List<Item> itemList;
        public List<Item> shoppingCart;
        public List<Item> purchasedItems;

        private static int _lastId = 0;
        private int _id;

        private ServicePoint _goToSP;

        //Super Rhudimentary State Machine
        enum CustomerState
        {
            Shopping, // buying items
            MainQueue, // waiting to get into service point queue
            ServicePointQueue, // waiting for people in front of him
            Purchasing //scanning items
        };
        CustomerState state;

        public Customer()
        {
            state = CustomerState.Shopping;
            _id = _lastId;
            _lastId++;
            Program.Debug("Customer Enters Store");

            itemList = Item.GenerateRandomItems();
            shoppingCart = new List<Item>();
            purchasedItems = new List<Item>();
        }

        //main function of the customer
        public void Begin()
        {
            Program.Debug("_id: I have " + itemList.Count + " Items!");
            while (true)
            {
                ProcessSelf();
            }
        }

        public void ProcessSelf()
        {
            switch (state)
            {
                case CustomerState.Shopping:
                    //Find Random Item
                    lock (this)
                    {
                        Item found = itemList[Store.rand.Next(0, itemList.Count)];
                        itemList.RemoveAt(Store.rand.Next(0, itemList.Count));
                        shoppingCart.Add(found);
                        Program.Debug(found.ToString());
                    }
                    System.Threading.Thread.Sleep(2000);
                    if (itemList.Count == 0)
                    {
                        state = CustomerState.MainQueue;
                        Queue<Customer> l = Store.Get().MainQueue;
                        lock (l)
                        {
                            l.Enqueue(this);
                        }
                    }
                    break;
                case CustomerState.MainQueue:
                    if (Store.Get().MainQueue.Peek() == this)
                    {
                        //Try to go to service Point
                        Store.Get().SPS.RegisterObserver(this);
                        //Pretend to update once
                        OnSPSUpdate();
                    }
                    System.Threading.Thread.Sleep(100);
                    break;
                case CustomerState.ServicePointQueue:
                    break;
                case CustomerState.Purchasing:
                    break;
             }
        }

        //Logic to go to our favorite SP given free SPs
        private void _selectFavoriteSP(List<ServicePoint> sp)
        {
            foreach (ServicePoint s in sp)
            {
                //distance
                float distance = Math.Sqrt
            }
        }

        public void OnSPSUpdate()
        {
            List<ServicePoint> sp = Store.Get().SPS.GetAvailableSP();
            if (sp != null)
            {
                lock (_goToSP)
                {
                    _goToSP = sp;
                }
            }
        }
    }
}
