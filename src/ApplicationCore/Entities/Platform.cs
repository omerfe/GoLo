using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Platform: BaseEntity
    {
        public string PlatformName { get; set; }
        public string LogoPath { get; set; }
        public List<Product> Products { get; set; }
    }
}
