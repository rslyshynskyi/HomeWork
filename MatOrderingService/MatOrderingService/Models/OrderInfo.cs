﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatOrderingService.Domain;

namespace MatOrderingService.Models
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string Status { get; set; }
        public string OrderCode { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
    }
}
