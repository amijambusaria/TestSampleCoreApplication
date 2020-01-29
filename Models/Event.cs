using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCoreApplication.Models
{
    public class Event
    {
        
        public int EventId { get; set; }
        public string Name { get; set; }
        public int TimeoutInSeconds { get; set; }
    }
}
