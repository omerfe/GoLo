using System;

namespace ApplicationCore.Entities
{
    public class Key : BaseEntity
    {
        public Guid KeyCode { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}