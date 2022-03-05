using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Key: BaseEntity
    {
        public Guid KeyCode { get; set; }
        public bool Status { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<Cart> Carts { get; set; } //TODO: Burası Düşünülecek!!!
    }
}
