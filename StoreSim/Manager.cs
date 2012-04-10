using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StoreSim
{
    class Manager : iSPSObserver
    {
        public int additionalCashier;

        enum ManagerState
        {
            TakingBreak,
            Thinking, // Doing nothing but collects information to decide what to do 
            OpeningStore, // Opening Store
            ManagingCashier, //Managing Cashier
            ClosingStore, // Closing Store
        };
        ManagerState state;
        public Manager()
        {
            additionalCashier = 0;
            state = ManagerState.Thinking;
            //maxNumberCashier = Store.Get().StoreParams.MaximumServicePoints;
            Store.Get().SPS.RegisterObserver(this); //Manager needs to be registerd here too!!
            Thread t = new Thread(new ThreadStart(this.Begin));
            //t.Priority = ThreadPriority.AboveNormal;
            t.Start();
        }

        public void Begin()
        {
            //Program.Debug("Manager is Created.");
            while (true)
            {
                //System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer);
                ProcessSelf();
                //Program.Debug("Manager print this");
            }
        }

        public void ProcessSelf()
        {
            System.Threading.Thread.Sleep(10);
            switch (state)
            {
                case ManagerState.Thinking:
                    //Think what to do
                    if (Store.Get().open == false)
                    {
                        state = ManagerState.OpeningStore;
                    }
                    else //the case the store is already opened!
                    {
                        //if (Store.Get().CustomerPool.Count > 5)
                        //{
                            state = ManagerState.ManagingCashier;
                        //}
                        //else if (Store.Get().CustomerPool.Count == 0)
                        //{
                        //    state = ManagerState.ClosingStore;
                        //}
                    }
                    break;
                case ManagerState.TakingBreak:
                    if (!Store.Get().StoreParams.ManagerCanBreak)
                    {
                        state = ManagerState.Thinking;
                        break;
                    }
                    //Wait for Finding Item
                    Program.Debug("Manager -> Breaking!!!");
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.ManagerBreakTime); ///////////////////////////////////
                    Program.Debug("Manager -> Not Breaking!!!");
                    state = ManagerState.Thinking;
                    break;
                case ManagerState.OpeningStore:
                    Store.Get().open = true;
                    Program.Debug("Open up the Store");
                    System.Threading.Thread.Sleep(100);
                    state = ManagerState.Thinking;
                    break;
                case ManagerState.ClosingStore:
                    //need to get rid of all of the customers. NOOOOOOOOOOOOOOOOOOOOOOOOO
                    //need to not receive any more customers!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                   // Store.Get().open = false;
                   // Program.Debug("Store Closed");
                   // System.Threading.Thread.Sleep(15000); //sleep 50 sec
                    // state = ManagerState.TakingBreak;
                    state = ManagerState.Thinking;
                    break;
                case ManagerState.ManagingCashier:
                    AdjustNumberOfCashier();
                    System.Threading.Thread.Sleep(Store.Get().StoreParams.ManagerBreakTime); 
                    state = ManagerState.Thinking;
                    break;
                
            }
        }

        bool outOfDate = false;
        public void OnSPSUpdate()
        {
            outOfDate = true;
        }

        public void AdjustNumberOfCashier()
        {
            List<ServicePoint> sp = Store.Get().SPS.GetServicePoints();//get all the available SPs.
            string adjustState = _needToAdjust(sp);
            if (adjustState.Equals("increase") && Store.Get().SPS.GetOpenedSP().Count < Store.Get().StoreParams.MaximumServicePoints)
            {
                for (int i = 0; i < sp.Count; i++)
                {
                    if (Store.Get().SPS.GetServicePoints().ElementAt(i).Opened == false)
                    {
                        lock (Store.Get().SPS)
                        {
                            Program.Debug("Manager -> Opening Cashier #" + i);
                            Store.Get().SPS.OpenServicePoint(Store.Get().SPS.GetServicePoints().ElementAt(i));
                        }
                        break;
                    }
                }
                
            }
            else if (adjustState.Equals("decrease") && Store.Get().SPS.GetOpenedSP().Count > Store.Get().StoreParams.MinimumServicePoints)
            {
                ServicePoint leastAmountCashier = null;
                //look for the least amount
                for (int i = 0; i < sp.Count; i++)
                {
                    if (sp.ElementAt(i).Opened)
                    {
                        if (leastAmountCashier == null || leastAmountCashier.GetNumberOfCustomers() < sp.ElementAt(i).GetNumberOfCustomers())
                            leastAmountCashier = sp.ElementAt(i);
                    }
                }
                if (leastAmountCashier != null)
                {
                    Program.Debug("Manager -> Closing Cashier #" +leastAmountCashier.ID);
                    Store.Get().SPS.CloseServicePoint(leastAmountCashier);
                }

            }
            else if (adjustState.Equals("nothing"))
            {
                //need to increase the stress point by 1.
            }
        }

        public string _needToAdjust(List<ServicePoint> servicePoints)
        {
            int count = Store.Get().MainQueue.Count;
            if (count > 4) /////////////////////////////*************************************Add value here!!
            {
                return "increase"; ////////////////////////////////////2 is wrong****************************************
            }
            else if (count < 4)
            {
                return "decrease";
            }
            else
                return "nothing";
        }

    }
}
