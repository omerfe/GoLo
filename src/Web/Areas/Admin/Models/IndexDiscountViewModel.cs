using ApplicationCore.Entities;
using System.Collections.Generic;

namespace Web.Areas.Admin.Models
{
    public class IndexDiscountViewModel
    {
        public int ProductId { get; set; }
        public string PlatformName { get; set; }
        public string GameName { get; set; }
        public List<Discount> Discounts { get; set; } = new List<Discount>();
    }
}