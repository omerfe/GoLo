using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class CartWithItemsSpecification : Specification<Cart>
    {
        public CartWithItemsSpecification(int cartId)
        {
            Query.Where(c => c.Id == cartId)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product.Game)
                .ThenInclude(g=> g.Products).ThenInclude(pr=> pr.Platform).ThenInclude(pl=>pl.Products).ThenInclude(pr => pr.Discounts);
            //Query.Include(x => x.CartItems)
            //    .ThenInclude(x => x.Product.Platform);
            //Query.Include(x => x.CartItems)
            //    .ThenInclude(x => x.Product.Discounts);
        }
        public CartWithItemsSpecification(string buyerId)
        {
            Query.Where(x => x.BuyerId == buyerId)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product);
        }
    }
}
