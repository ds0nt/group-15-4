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
            Program.debug("Customer Enters Store");

            itemList = Item.GenerateRandomItems();
            shoppingCart = new List<Item>();
            purchasedItems = new List<Item>();
        }

        //main function of the customer
        public void Begin()
        {
            Program.debug("_id: I have " + itemList.Count + " Items!");
            while (true)
            {
                processSelf();
            }
        }

        public void processSelf()
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
                        Program.debug(found.ToString());
                    }
                    System.Threading.Thread.Sleep(2000);
                    if (itemList.Count == 0)
                    {
                        state = CustomerState.MainQueue;
                        Queue<Customer> l = Store.get().MainQueue;
                        lock (l)
                        {
                            l.Enqueue(this);
                        }
                    }
                    break;
                case CustomerState.MainQueue:
                    if (Store.get().MainQueue.Peek() == this)
                    {
                        //Try to go to service Point
                        Console.WriteLine("IM AT THE FRONT BITCHES");
                        
                    }
                    break;
                case CustomerState.ServicePointQueue:
                    break;
                case CustomerState.Purchasing:
                    break;
             }
        }

        public void onSPSUpdate()
        {
            ServicePoint sp = Store.get().SPS.getAvailableSP();
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
