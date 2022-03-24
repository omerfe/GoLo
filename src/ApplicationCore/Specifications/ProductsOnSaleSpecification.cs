using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductsOnSaleSpecification : Specification<Product>
    {
        public ProductsOnSaleSpecification()
        {
            Query.Include(x => x.Game);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Platform);
            Query.Where(x => x.IsAvailable);
            Query.Where(x => x.Keys.Where(y => y.Status).ToList().Count > 0);
            Query.Where(x => x.Discounts.Any(x => x.ValidUntil.Date >= DateTime.Now.Date && x.ValidFrom.Date <= DateTime.Now.Date));
            Query.Take(10);
        }

    }
}
