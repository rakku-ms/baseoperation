using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseOperations.Models
{
    public class Event
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public string ImageURL { get; set; }
        public bool? ContainsTruck { get; set; }
        public string Location { get; set; }
    }
}
