using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCoreApplication.Models
{
    public class Buyer
    {
        public int BuyerId { get; set; }

        public int EventId { get; set; }

        public string TesterKey { get; set; }

        [Required]
        public string BuyerName { get; set; }
    }
}
