using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models
{
    public class FilterViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Platforms { get; set; }

        public int? GenreId { get; set; }
        public int? PlatformId { get; set; }


    }
}
