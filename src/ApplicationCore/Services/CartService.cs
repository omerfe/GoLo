using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{

    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<CartItem> _cartItemRepo;

        public CartService(IRepository<Cart> cartRepo, IRepository<Product> productRepo, IRepository<CartItem> cartItemRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _cartItemRepo = cartItemRepo;
        }
        public async Task<Cart> AddItemToCartAsync(int cartId, int productId, int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be greater than zero.");
            var productSpec = new ProductsDetailSpecification(productId);
            var product = await _productRepo.FirstOrDefaultAsync(productSpec);
            if (product == null)
                throw new ArgumentException($"Product with the id {productId} can not be found.");
            var spec = new CartWithItemsSpecification(cartId);
            var cart = await _cartRepo.FirstOrDefaultAsync(spec);
            if (cart == null)
                throw new ArgumentException($"Cart with id {cartId} can not be found.");

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    CartId = cartId,
                    Quantity = quantity,
                    ProductId = productId,
                    Product = product
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            await _cartRepo.UpdateAsync(cart);

            return cart;
        }

        public async Task DeleteCartAsync(int cartId)
        {
            var cart = await _cartRepo.GetByIdAsync(cartId);
            if (cart == null)
                throw new ArgumentException($"Cart with id {cartId} can not be found.");

            await _cartRepo.DeleteAsync(cart);
        }

        public async Task DeleteCartItemAsync(int cartId, int cartItemId)
        {
            var cartItem = await _cartItemRepo.GetByIdAsync(cartItemId);
            if (cartItem == null || cartItem.CartId != cartId)
                throw new ArgumentException("Cart item can not be found");

            await _cartItemRepo.DeleteAsync(cartItem);
        }

        public async Task<Cart> SetQuantitesAsync(int cartId, Dictionary<int, int> quantities)
        {
            if (quantities.Values.Any(x => x < 1))
                throw new ArgumentException("All quantities must be greater than zero.");

            var spec = new CartWithItemsSpecification(cartId);
            var cart = await _cartRepo.FirstOrDefaultAsync(spec);
            if (cart == null)
                throw new ArgumentException($"Cart with id {cartId} can not be found.");

            foreach (var item in cart.CartItems)
            {
                if (quantities.ContainsKey(item.ProductId))
                {
                    item.Quantity = quantities[item.ProductId];
                }
            }
            await _cartRepo.UpdateAsync(cart);
            return cart;
        }

        public async Task TransferCartAsync(string anonymousId, string userId)
        {
            var specAnon = new CartWithItemsSpecification(anonymousId);
            var anonCart = await _cartRepo.FirstOrDefaultAsync(specAnon);
            if (anonCart == null) return;

            var specUser = new CartWithItemsSpecification(userId);
            var userCart = await _cartRepo.FirstOrDefaultAsync(specUser);
            if (userCart == null)
            {
                userCart = new Cart() { BuyerId = userId, CartItems = new List<CartItem>() };
                await _cartRepo.AddAsync(userCart);
            }

            foreach (CartItem item in anonCart.CartItems)
            {
                CartItem targetItem = userCart.CartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (targetItem != null)
                    targetItem.Quantity += item.Quantity;
                else
                    userCart.CartItems.Add(new CartItem() { ProductId = item.ProductId, Quantity = item.Quantity });
            }
            await _cartRepo.UpdateAsync(userCart);
            await _cartRepo.DeleteAsync(anonCart);
        }
    }
}
