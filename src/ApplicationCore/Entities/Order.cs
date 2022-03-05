using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Order: BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;  
        public decimal TotalPrice { get; set; }
    }
}
