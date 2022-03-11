using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductsDetailSpecification : Specification<Product>
    {
        public ProductsDetailSpecification(int productId)
        {
            if (productId > 0)
            {
                Query.Include(x => x.Game.Genres);
                Query.Include(x => x.Discounts);
                Query.Include(x => x.Platform);
                Query.Where(x => x.Id == productId);

            }
        }
    }
}
