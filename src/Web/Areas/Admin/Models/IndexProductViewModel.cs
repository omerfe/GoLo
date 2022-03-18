using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class IndexProductViewModel
    {
        public int Id { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public bool IsAvailable { get; set; }
        public int UnitInStocks { get; set; }
        public int CurrentDiscountRate { get; set; }
        public Game Game { get; set; }
        public Platform Platform { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Key> Keys { get; set; }
    }
}
