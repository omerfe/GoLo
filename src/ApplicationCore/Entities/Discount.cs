using System;

namespace ApplicationCore.Entities
{
    public class Discount : BaseEntity
    {
        public int DiscountRate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsValid => (ValidUntil >= DateTime.Now && ValidFrom <= DateTime.Now);
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}