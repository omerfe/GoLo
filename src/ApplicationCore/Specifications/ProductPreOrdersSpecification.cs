using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductPreOrdersSpecification : Specification<Product>
    {
        public ProductPreOrdersSpecification()
        {
            Query.Include(x => x.Game);
            Query.Include(x => x.Discounts);
            Query.Where(x => x.Game.ReleaseDate > DateTime.Now).OrderBy(x => x.Game.ReleaseDate);
            Query.Take(10);
        }
    }
}
