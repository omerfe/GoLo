using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductsEditorsChoiceSpecification : Specification<Product>
    {
        public ProductsEditorsChoiceSpecification()
        {
            Query.Include(x => x.Game.Genres);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Platform);
            Query.Where(x => x.IsAvailable);
            Query.Where(x => x.Keys.Where(y => y.Status).ToList().Count > 0);
            Query.Where(x => x.IsEditorsChoice);
            Query.Take(10);
        }
    }
}
