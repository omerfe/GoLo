using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Cart : BaseEntity
    {
        public List<CartItem> CartItems { get; set; }
        public string BuyerId { get; set; }
    }
}