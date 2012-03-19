using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreSim
{
    struct StoreParams
    {
        public int MaximumServicePoints { get; set; } //C-01
        public int MinimumServicePoints { get; set; } //C-04
        public int InitialServicePoints { get; set; } //C-11
        public int QueueMaxSize { get; set; } //C-12
    }
}
