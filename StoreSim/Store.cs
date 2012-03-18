using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Store
    {
        private static Store myInstance;

        public Store()
        {
            myInstance = this;
        }

        public static Store get()
        {
            if (myInstance == null)
                myInstance = new Store();

            return myInstance;
        }

        public void simulate()
        {

        }
    }
}
