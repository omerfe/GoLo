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

        public string SearchText { get; set; }
        public int? SortItem { get; set; }
        public List<SelectListItem> SortTypes { get; set; }
        
        
        public PaginationInfoViewModel PaginationInfo { get; set; }

        public string HrefMaker(string query,int pageNo, string controller, string action)
        {
            var indexP = query.IndexOf("p=");
            var index = query.IndexOf('&');
            string newQuery = query;
            var route = "";
            if (newQuery == "")
            {
                return $"/{controller}/{action}/?p={pageNo}";
            }
            else if (!newQuery.Contains("Genre") && !newQuery.Contains("Platform") && !newQuery.Contains("Search") && !newQuery.Contains("Sort"))
            {
                return $"/{controller}/{action}/?p={pageNo}";
            }
            else
            {
                if (indexP > 0)
                {
                    newQuery = query.Remove(indexP - 1, index - indexP + 1);
                }
                return route = $"/{controller}/{action}/?p={pageNo}&{newQuery.Remove(0, 1)}";
            }
        }
    }
}
