using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertTrackerAPI
{
    public class Vendor
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public int VisitorCount { get; set; }
        public DateTime LastVisted { get; set; }
    }
}
