using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        public decimal ProductUnitPrice { get; set; }
        public int UnitInStocks { get; set; }
        public int PlatformId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public Platform Platform { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Key> Keys { get; set; }
    }
}
