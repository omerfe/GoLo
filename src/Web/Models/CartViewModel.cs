using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public int TotalItemsCount => Items.Sum(x => x.Quantity);
        public decimal TotalPrice => Items.Sum(x => x.SubTotalPrice);
    }
}
