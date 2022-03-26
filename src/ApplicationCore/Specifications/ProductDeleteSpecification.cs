using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class ProductDeleteSpecification : Specification<Product>
    {
        public ProductDeleteSpecification(int productId)
        {

            Query.Where(x => x.Id == productId)
                .Include(x => x.Keys);
        }
    }
}
