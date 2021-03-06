﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Item
    {
        public int ItemNum { get; set; }
        public double Price { get; set; }
        
        public Item() 
        {

        }

        //Creates a random Item
        private static Item RandomItem()
        {
            return new Item() { 
                ItemNum = Store.rand.Next(),
                Price = Store.rand.NextDouble() * 100
            };
        }


        //Creates A bunch of random items
        public static List<Item> GenerateRandomItems(int items = 0)
        {
            List<Item> ret = new List<Item>();
            
            if(items == 0)
                items = Store.rand.Next(Store.Get().StoreParams.RandomItemMin, Store.Get().StoreParams.RandomItemMax);

            for (int i = 0; i < items; i++)
                ret.Add(RandomItem());

            return ret;
        }

        public override string ToString()
        {
            return "Item(" + ItemNum + ": " + Price + ")";
        }
    }
}
