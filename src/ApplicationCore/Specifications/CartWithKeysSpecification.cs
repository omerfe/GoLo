using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class CartWithKeysSpecification : Specification<Cart>
    {
        public CartWithKeysSpecification(int cartId)
        {
            Query.Where(c => c.Id == cartId)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product.Game);

            Query.Where(c => c.Id == cartId)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product.Platform);

            Query.Where(c => c.Id == cartId)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product.Discounts);

            Query.Where(c => c.Id == cartId)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product.Keys);
        }
    }
}
//.ThenInclude(g => g.Products)
//.ThenInclude(pr => pr.Platform)
//.ThenInclude(pl => pl.Products)
//.ThenInclude(pr => pr.Discounts)
//.ThenInclude(d => d.Product)
//.ThenInclude(pr => pr.Keys);