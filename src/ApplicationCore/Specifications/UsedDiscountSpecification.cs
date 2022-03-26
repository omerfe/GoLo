using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class UsedDiscountSpecification : Specification<Discount>
    {
        public UsedDiscountSpecification(int discountId)
        {
            Query.Where(x => x.Id == discountId)
                .Include(x => x.Product.Keys);
        }
    }
}
