using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StoreSim
{
    class Manager : iSPSObserver
    {
        public int maxNumberCashier;
        public int currentAvailableCashier;


        enum ManagerState
        {
            TakingBreak,
            Thinking, // Doing nothing but collects information to decide what to do 
            OpeningStore, // Opening Store
            ManagingCashier, //Closing Cashier
            ClosingStore, // Closing Store
        };
        //public struct ManagerStart
        //{
         //   public int maxNumberCashier;
            //public int delay;
        //};
        ManagerState state;
        public Manager()
        {
            state = ManagerState.TakingBreak;
            maxNumberCashier = Store.Get().StoreParams.MaximumServicePoints;






            new Thread(new ThreadStart(this.Begin)).Start();
        }

        public void Begin()
        {
            //Thread.Sleep((int)(_delay / Store.Get().StoreParams.TimeScale));
            Program.Debug("Manager is Created.");
            while (true)
            {
                ProcessSelf();
            }
        }

        public void ProcessSelf()
        {
            switch (state)
            {
                case ManagerState.TakingBreak:
                    //Wait for Finding Item
                    Program.Debug("MANAGER IS TAKING REST!!!");
                    System.Threading.Thread.Sleep(10000); ///////////////////////////////////
                    state = ManagerState.OpeningStore;
                    break;
                case ManagerState.OpeningStore:
                    Store.Get().open = true;
                    break;
                case ManagerState.Thinking:
                    //Think what to do
                    if (Store.Get().open == false)
                    {
                        //Open up the store
                        state = ManagerState.OpeningStore;
                        // -- Start observing the Service Points
                        Store.Get().SPS.RegisterObserver(this); //Manager needs to be registerd here too!!
                        //Check what's going on there.
                        OnSPSUpdate(); //this is meaningless but u kno, just for fun.
                    }
                    else //the case the store is already opened!
                    {                                           //*****************************************************
                        if (Store.Get().CustomerPool.Count > 5) //////////////////////////////////////FIX THIS!!!!!!!!!!!!!!!!!
                            state = ManagerState.ManagingCashier;//******************************************************** 
                                                                //If there is more than 5 ppl in the store.
                        else
                            state = ManagerState.ClosingStore;
                    }
                    break;
                case ManagerState.ManagingCashier:
                    OnSPSUpdate();
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
            }
        }

        public void OnSPSUpdate()
        {
            List<ServicePoint> sp = Store.Get().SPS.GetAvailableSP(); //get all the available SPs.

            //ServicePoint s = _selectFavoriteSP(sp);
            if (_needToAdjust(sp) == "increase")
            {
                ServicePoint newServicePoint = new ServicePoint();
                lock (this)
                {
                    lock (Store.Get().SPS)
                    {
                        Program.Debug("************************************************CASHIER ++ **********");
                        Store.Get().SPS.AddServicePoint(newServicePoint);
                    }
                }
            }
            else if(_needToAdjust(sp) == "decrease")
            {
                ServicePoint leastAmountCashier = new ServicePoint();
           
                lock (this)
                {
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
                    lock (leastAmountCashier)
                    {
                        Program.Debug("--------------------------------------------- DECREASED!!");
                        Store.Get().SPS.CloseServicePoint(leastAmountCashier);
                    }
                }
            }
            else if (_needToAdjust(sp) == "nothing")
            {
                //need to increase the stress point by 1.
            }
        }
        public string _needToAdjust(List<ServicePoint> servicePoints)
        {
            //Actually, here it adjusts. Add/remove service points
            int averageNumberOfCustomer = 0;
            for (int i = 0; i < servicePoints.Count; i++)
            {
                averageNumberOfCustomer += servicePoints.ElementAt(i).GetNumberOfCustomers();
            }
            averageNumberOfCustomer /= servicePoints.Count;


            if (averageNumberOfCustomer >2) /////////////////////////////*************************************Add value here!!
                return "increase"; ////////////////////////////////////2 is wrong****************************************
            else if ((averageNumberOfCustomer <2) && (servicePoints.Count > Store.Get().StoreParams.MinimumServicePoints))
                return "decrease";
            else
                return "nothing";
        }

    }
}
