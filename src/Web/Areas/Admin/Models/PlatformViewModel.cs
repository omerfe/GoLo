using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class PlatformViewModel
    {
        public int Id { get; set; }
        public string PlatformName { get; set; }
        public string LogoPath { get; set; }
        public IFormFile LogoImage { get; set; }
    }
}
