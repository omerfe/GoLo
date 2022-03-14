using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Cart : BaseEntity
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public string BuyerId { get; set; }
    }
}