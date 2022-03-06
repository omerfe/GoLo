using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
