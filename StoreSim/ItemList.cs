using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Item
    {
        private int itemNum;
        private float price;
        public Item()   //Item List constructor
        {
            itemNum = 0;
            price = 0.0f;
        }

        public static List<Item> generateRandomItems()
        {
            List<Item> ret = new List<Item>();
            return ret;
        }
    }
}
