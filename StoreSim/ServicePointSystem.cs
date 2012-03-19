using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    class ServicePointSystem : iSPObserver
    {
        private List<ServicePoint> _spList;
        private List<IObserver<ServicePointSystem>> _observers;

        public ServicePointSystem()
        {
            for (int i = 0; i < Store.get().StoreParams.InitialServicePoints; i++)
            {

            }
        }

        public void AddServicePoint(ServicePoint c)
        {

        }

        public void CloseServicePoint(ServicePoint p)
        {

        }

        //
        public void onSPUpdate()
        {

        }
    }
}
