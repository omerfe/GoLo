using System;

namespace Web.Areas.Admin.Models
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        public int DiscountRate { get; set; }
        public int ProductId { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}