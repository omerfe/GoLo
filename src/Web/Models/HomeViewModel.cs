using System.Collections.Generic;

namespace Web.Models
{
    public class HomeViewModel
    {
        public List<ProductViewModel> ProductsOnSale { get; set; }
        public List<ProductViewModel> ProductsPreOrders { get; set; }
        public List<ProductViewModel> ProductsNewRelease { get; set; }
        public List<ProductViewModel> ProductsEditorsChoice { get; set; }
        
    }
}
