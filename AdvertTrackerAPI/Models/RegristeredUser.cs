using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertTrackerAPI.Models
{
    public class RegristeredUser
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}
