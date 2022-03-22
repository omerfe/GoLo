using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification(string buyerId)
        {
            Query.Where(x => x.BuyerId == buyerId)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Key)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Game);
            Query.Where(x => x.BuyerId == buyerId)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Key)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Platform);
        }
    }
}
