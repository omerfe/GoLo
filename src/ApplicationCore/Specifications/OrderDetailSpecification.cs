using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class OrderDetailSpecification : Specification<OrderDetail>
    {
        public OrderDetailSpecification(int discountId)
        {
            Query.Where(x => x.OrderDiscountId.Value == discountId);
        }
    }
}
