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
            state = ManagerState.TakingBreak;
            //maxNumberCashier = Store.Get().StoreParams.MaximumServicePoints;
            Store.Get().SPS.RegisterObserver(this); //Manager needs to be registerd here too!!
            new Thread(new ThreadStart(this.Begin)).Start();
        }

        public void Begin()
        {
            Program.Debug("Manager is Created.");
            while (true)
            {
                System.Threading.Thread.Sleep(Store.Get().StoreParams.ReactionTimeCustomer);
                ProcessSelf();
                //Program.Debug("Manager print this");
            }
        }

        public void ProcessSelf()
        {
            switch (state)
            {
                case ManagerState.Thinking:
                    //Think what to do
                    if (Store.Get().open == false)
                    {
                        state = ManagerState.OpeningStore;
                    }
                    else //the case the store is already opened!
                    {                                           //*****************************************************
                        //if (Store.Get().CustomerPool.Count < 5) //////////////////////////////////////FIX THIS!!!!!!!!!!!!!!!!!
                            state = ManagerState.ManagingCashier;//******************************************************** 
                        //else
                        //    state = ManagerState.ClosingStore;
                    }
                    break;
                case ManagerState.TakingBreak:
                    //Wait for Finding Item
                    Program.Debug("MANAGER IS TAKING REST!!!");
                    System.Threading.Thread.Sleep(0); ///////////////////////////////////
                    state = ManagerState.Thinking;
                    break;
                case ManagerState.OpeningStore:
                    Store.Get().open = true;
                    Program.Debug("Open up the Store");
                    //System.Threading.Thread.Sleep(100);
                    state = ManagerState.Thinking;
                    break;
                case ManagerState.ClosingStore:
                    //need to get rid of all of the customers. NOOOOOOOOOOOOOOOOOOOOOOOOO
                    //need to not receive any more customers!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Store.Get().open = false;
                    Program.Debug("Store Closed");
                    System.Threading.Thread.Sleep(15000); //sleep 50 sec
                    state = ManagerState.TakingBreak;
                    break;
                case ManagerState.ManagingCashier:
                    //AdjustNumberOfCashier();
                    //Program.Debug("Is this printed???????????????????????????????????");
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
            //Program.Debug("Is this printed??");
            List<ServicePoint> sp = Store.Get().SPS.GetServicePoints();//get all the available SPs.

            if (_needToAdjust(sp) == "increase") //&& Store.Get().SPS.GetAvailableSP().Count < Store.Get().StoreParams.MaximumServicePoints)
            {
                Program.Debug("Is this printed??");
                /*for (int i = 0; i < sp.Count; i++)
                {
                    if (sp.ElementAt(i).Opened == false)
                    {
                        lock (Store.Get().SPS)
                        {
                            Program.Debug("*******************************************CASHIER Opened **");
                            Store.Get().SPS.OpenServicePoint(sp.ElementAt(i));//sp.ElementAt(i).Start();
                        }
                        break;
                    }
                }*/
                    
            }
            else if(_needToAdjust(sp) == "decrease" )//&& sp.Count > Store.Get().StoreParams.MinimumServicePoints)
            {
                Program.Debug("WTF!?");
                /*
                ServicePoint leastAmountCashier = null;
           
                //look for the least amount
                for (int i = 0; i < sp.Count; i++)
                {
                    if (i == 0)
                        leastAmountCashier = sp.ElementAt(0);
                    else
                    {
                        if (sp.ElementAt(i - 1).GetNumberOfCustomers() > sp.ElementAt(i).GetNumberOfCustomers())
                            leastAmountCashier = sp.ElementAt(i);
                        else
                            leastAmountCashier = sp.ElementAt(i - 1);
                    }
                }
                if (leastAmountCashier != null)
                {
                    Store.Get().SPS.CloseServicePoint(leastAmountCashier);
                    Program.Debug("----------------------------        Cachier Closed!");
                }
                 */

            }
            else if (_needToAdjust(sp) == "nothing")
            {
                //Program.Debug("FUCK????????");
                //need to increase the stress point by 1.
                 
            }
        }

        public string _needToAdjust(List<ServicePoint> servicePoints)
        {

            if (Store.Get().MainQueue.Count > 1) /////////////////////////////*************************************Add value here!!
            {
                //additionalCashier++;
                return "increase"; ////////////////////////////////////2 is wrong****************************************
            }
            else if (Store.Get().MainQueue.Count < 4 && Store.Get().StoreParams.MinimumServicePoints == Store.Get().SPS.GetAvailableSP().Count)
            {
                //additionalCashier--;
                return "decrease";
            }
            else
                return "nothing";
        }

    }
}
