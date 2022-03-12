using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICartService
    {
        Task<Cart> AddItemToCartAsync(int cartId, int productId, int quantity);
        Task<Cart> SetQuantitesAsync(int cartId, Dictionary<int, int> quantities);
        Task DeleteCartAsync(int cartId);
        Task DeleteCartItemAsync(int cartId, int cartItemId);
        Task TransferCartAsync(string anonymousId, string userId);
    }
}
