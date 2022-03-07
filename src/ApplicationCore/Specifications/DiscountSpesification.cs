using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class DiscountSpesification : Specification<Discount>
    {
        public DiscountSpesification(int? productId)
        {
            if (productId.HasValue)
            {
                Query.Where(x => x.ProductId == productId);
                Query.Where(x => x.IsValid);
            }
        }
    }
}
