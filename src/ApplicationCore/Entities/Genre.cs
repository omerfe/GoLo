using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Genre : BaseEntity
    {
        public string CategoryName { get; set; }
        public List<Game> Games { get; set; }
    }
}
