namespace ApplicationCore.Entities
{
    public class OrderDetails : BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public string GameName { get; set; }
        public string ImagePath { get; set; }
        public int? OrderDiscountId { get; set; }
        public int OrderId { get; set; }
        public int KeyId { get; set; }
        public Order Order { get; set; }
        public Key Key { get; set; }
    }
}