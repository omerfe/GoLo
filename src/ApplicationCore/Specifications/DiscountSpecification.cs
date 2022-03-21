using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class DiscountSpecification : Specification<Discount>
    {
        public DiscountSpecification(int productId)
        {
            Query.Where(x => x.ProductId == productId)
                .Include(x => x.Product)
                .ThenInclude(p => p.Platform);
            Query.Where(x => x.ProductId == productId)
                .Include(x => x.Product)
                .ThenInclude(p => p.Game);
        }

        public DiscountSpecification(int productId, DateTime validFrom, DateTime validUntil)
        {
            Query.Where(x => x.ProductId == productId);
            Query.Where(x => validUntil >= x.ValidFrom && x.ValidUntil >= validFrom);
        }
    }
}