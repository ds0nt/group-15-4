using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Item
    {
        public int ItemNum { get; set; }
        public double Price { get; set; }
        public Item()   //Item List constructor
        {

        }

        private static Item RandomItem()
        {
            return new Item() { 
                ItemNum = Store.rand.Next(),
                Price = Store.rand.NextDouble() * 100
            };
        }

        public static List<Item> GenerateRandomItems()
        {
            List<Item> ret = new List<Item>();
            
            //Parabolic Probability
            int itemCount = Store.rand.Next(1, 5) * Store.rand.Next(1, 5);
            for (int i = 0; i < itemCount; i++)
                ret.Add(RandomItem());

            return ret;
        }

        public override string ToString()
        {
            return "Item(" + ItemNum + ": " + Price + ")";
        }
    }
}
