using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Customer : iSPSObserver
    {
        List<Item> itemList;
        private static int _lastId = 0;
        private int _id;

        public Customer()
        {
            _id = _lastId;
            _lastId++;
            Program.debug("Customer Enters Store");
            itemList = new List<Item>();
            //add random items to list
        }

        //main function of the customer
        public void begin()
        {
            while (true)
            {
                Program.debug("(" +_id + ") Customer wooo");
            }
        }

        public void onSPSUpdate()
        {

        }
    }
}
