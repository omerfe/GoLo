using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Discount : BaseEntity
    {
        public int DiscountRate { get; set; }
        public DateTime ValidFrom { get; set; } 
        public DateTime ValidUntil { get; set; }
        public bool IsValid => (ValidUntil >= DateTime.Now && ValidFrom <= DateTime.Now); //TODO: IsValid Çalışır mı?
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
