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
            Query.Where(x => x.Id == cartId)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product);
        }
        public CartWithItemsSpecification(string buyerId)
        {
            Query.Where(x => x.BuyerId == buyerId)
            .Include(x => x.CartItems)
            .ThenInclude(x => x.Product);
        }
    }
}
