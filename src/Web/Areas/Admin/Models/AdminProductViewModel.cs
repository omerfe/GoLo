using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models
{
    public class AdminProductViewModel
    {
        public int Id { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEditorsChoice { get; set; }
        public Platform Platform { get; set; }
        public int PlatformId { get; set; }
        public Game Game { get; set; }
        public int GameId { get; set; }
        public List<SelectListItem> AllGames { get; set; }
        public List<SelectListItem> AllPlatforms { get; set; }
    }
}
