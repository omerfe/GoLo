using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class KeyViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid KeyCode { get; set; }
    }
}
