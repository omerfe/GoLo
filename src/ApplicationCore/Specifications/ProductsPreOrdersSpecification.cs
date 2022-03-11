using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductsPreOrdersSpecification : Specification<Product>
    {
        public ProductsPreOrdersSpecification()
        {
            Query.Include(x => x.Game);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Platform);
            Query.Where(x => x.Game.ReleaseDate > DateTime.Now).OrderBy(x => x.Game.ReleaseDate);
            Query.Take(10);
        }
    }
}
