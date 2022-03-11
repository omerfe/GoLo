using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class CartViewModelService : ICartViewModelService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Cart> _cartRepo;
        private readonly ICartService _cartService;


        public CartViewModelService(IHttpContextAccessor httpContextAccessor, IRepository<Cart> cartRepo, ICartService cartService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepo = cartRepo;
            _cartService = cartService;
        }

        public async Task<CartViewModel> GetCartViewModelAsync()
        {
            var cartId = (await GetOrCreateCartAsync()).Id;
            var specCart = new CartWithItemsSpecification(cartId);
            var cart = await _cartRepo.FirstOrDefaultAsync(specCart);
            return CartToViewModel(cart);
        }

        public async Task<int> GetCartItemsCountAsync()
        {
            var cart = await GetCartViewModelAsync();
            return cart.TotalItemsCount;
        }

        public async Task<CartViewModel> AddToCartAsync(int productId, int quantity)
        {
            var cart = await GetOrCreateCartAsync();
            cart = await _cartService.AddItemToCartAsync(cart.Id, productId, quantity);

            return CartToViewModel(cart);
        }

        public async Task<CartViewModel> UpdateCartAsync(Dictionary<int, int> quantities)
        {
            var cart = await GetOrCreateCartAsync();
            cart = await _cartService.SetQuantitesAsync(cart.Id, quantities);

            return CartToViewModel(cart);
        }
        private CartViewModel CartToViewModel(Cart cart)
        {


            var vm = new CartViewModel()
            {
                Id = cart.Id,
                BuyerId = cart.BuyerId,
                Items = cart.CartItems.Select(x => new CartItemViewModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    GameName = x.Product.Game.GameName,
                    PictureUri = x.Product.Game.ImagePath,
                    UnitPrice = x.Product.Discounts.FirstOrDefault(x => x.IsValid) == null ? x.Product.ProductUnitPrice : 
                        (x.Product.ProductUnitPrice * (100 - x.Product.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate) / 100),
                    PlatformName = x.Product.Platform.PlatformName
                }).ToList()
            };

            return vm;
        }

        public async Task<Cart> GetOrCreateCartAsync()
        {
            var buyerId = await GetOrCreateBuyerIdAsync();
            var specCart = new CartSpecification(buyerId);
            var cart = await _cartRepo.FirstOrDefaultAsync(specCart);
            if (cart == null)
            {
                cart = new Cart()
                {
                    BuyerId = buyerId
                };
                await _cartRepo.AddAsync(cart);
            }
            return cart;
        }

        public async Task<string> GetOrCreateBuyerIdAsync()
        {
            var userId = GetLoggedInUserId();
            if (userId != null) return userId;

            var anonId = GetAnonymusBuyerId();
            if (anonId != null) return anonId;

            var newId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.CART_COOKIENAME, newId, new
            CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddDays(28)
            });
            return newId;
        }

        private string GetAnonymusBuyerId()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[Constants.CART_COOKIENAME];
        }

        private string GetLoggedInUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
