using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Models
{
    public class NewOrder
    {
        public NewOrderData[] NewOrderData { get; set; }
        public string CreatorId { get; set; }
    }
}
