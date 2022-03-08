using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models
{
    public class FilterViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Platforms { get; set; }

        //public int? GenreId { get; set; }
        //public int? PlatformId { get; set; }
        public List<int> GenreIds { get; set; } = new List<int>();
        public List<int> PlatformIds { get; set; } = new List<int>();

        public PaginationInfoViewModel PaginationInfo { get; set; }

        public string HrefMaker(string query,int pageNo)
        {
            var indexP = query.IndexOf("p=");
            var index = query.IndexOf('&');
            string newquery = query;
            var route = "";
            if (newquery == "")
            {
                return $"/Filter?p={pageNo}";
            }
            else if (!newquery.Contains("Genre") && !newquery.Contains("Platform"))
            {
                return $"/Filter?p={pageNo}";
            }
            else
            {
                if (indexP > 0)
                {
                    newquery = query.Remove(indexP - 1, index - indexP + 1);
                }
                return route = $"/Filter?p={pageNo}&{newquery.Remove(0, 1)}";
            }
        }
    }
}
