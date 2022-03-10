using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductOnSaleSpecification : Specification<Product>
    {
        public ProductOnSaleSpecification()
        {
            Query.Include(x => x.Game);
            Query.Include(x => x.Discounts);
            Query.Where(x => x.Discounts.Any(x => x.ValidUntil <= DateTime.Now && x.ValidFrom >= DateTime.Now));
            Query.Take(10);
        }

    }
}
