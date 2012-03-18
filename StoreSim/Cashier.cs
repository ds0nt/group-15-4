using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class Cashier: QSObservable
    {
        public Cashier()
        {
            //itself must be a Que with 2 as maximum.
        }
        public void CustomerCame()
        {
            //added to the end of the que
        }
        public void CustomerLeft()
        {
            //discard the customer of first position
        }
    }
}
