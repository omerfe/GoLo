using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
