using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int MinimumAge { get; set; }
        public string TrailerUrl { get; set; }
        public string ImagePath { get; set; }
        public string GameRequirements { get; set; }
        public int DiscountRate { get; set; }
        public decimal UnitPrice { get; set; }
        public string PlatformLogo { get; set; }

        public List<string> Genres { get; set; }
        public List<ProductViewModel> ProductsNewRelease { get; set; }



    }
}
