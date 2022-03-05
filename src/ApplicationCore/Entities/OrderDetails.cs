using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class OrderDetails: BaseEntity
    {
        public decimal UnitPrice { get; set; }
        public int? OrderDiscountId { get; set; }
    }
}
