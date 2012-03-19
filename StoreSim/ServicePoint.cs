using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class ServicePoint
    {
        private Queue<Customer> _queue;
        private List<iSPObserver> _observers;

        public void register(iSPObserver obs)
        {
            
        }

        public ServicePoint()
        {

        }
    }
}