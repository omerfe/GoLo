using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cart:BaseEntity
    {
        //public int UserId { get; set; }
        //public User User { get; set; }
        public List<Key> Keys { get; set; }
    }
}
