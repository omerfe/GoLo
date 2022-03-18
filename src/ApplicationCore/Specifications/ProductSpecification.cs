using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
   public class ProductSpecification : Specification<Product>
    {
        public ProductSpecification()
        {
            Query.Include(x => x.Platform);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Game);
            Query.Include(x => x.Keys);
        }
        public ProductSpecification(string platformName, string gameName )
        {
            Query.Where(x => x.Platform.PlatformName == platformName && x.Game.GameName == gameName);
        }
        public ProductSpecification(int productId)
        {
            Query.Where(x => x.Id == productId);
            Query.Include(x => x.Platform);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Game);
            Query.Include(x => x.Keys);
        }
    }
}
