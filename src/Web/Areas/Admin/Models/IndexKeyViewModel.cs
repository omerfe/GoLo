using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class IndexKeyViewModel
    {
        public int ProductId { get; set; }
        public string PlatformName { get; set; }
        public string GameName { get; set; }
        public List<Key> Keys { get; set; } = new List<Key>();

    }
}
