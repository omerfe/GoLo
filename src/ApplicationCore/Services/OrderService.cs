using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Cart> _cartRepo;
        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<Key> _keyRepo;

        public OrderService(IRepository<Cart> cartRepo, IRepository<Order> orderRepo, IRepository<Key> keyRepo)
        {
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _keyRepo = keyRepo;
        }
        public async Task<Order> CreateOrderAsync(int cartId, string email)
        {
            var spec = new CartWithKeysSpecification(cartId);
            var cart = await _cartRepo.FirstOrDefaultAsync(spec);
            if (cart == null)
                throw new ArgumentException($"Cart with id {cartId} can not be found.");
            if (cart.CartItems.Count == 0)
                throw new ArgumentException($"Cart does not contain any items.");

            Order order = new Order()
            {
                BuyerId = cart.BuyerId,
                OrderDate = DateTimeOffset.Now,
                OrderDetails = new List<OrderDetails>()
            };

            var outOfStock = 0;
            var cartItems = cart.CartItems;
            var cartItemsCount = cartItems.Count;
            for (int i = 0; i < cartItemsCount; i++)
            {
                var availableKeysCount = cartItems[i].Product.Keys.Where(k => k.Status == true).ToList().Count();
                if (cartItems[i].Quantity > availableKeysCount)
                {
                    outOfStock++;
                    cartItems[i].Quantity = availableKeysCount;
                }
                if (cartItems[i].Quantity == 0)
                    cart.CartItems.Remove(cartItems[i]);
            }

            if (outOfStock > 0)
            {
                await _cartRepo.UpdateAsync(cart);
                return null;
            }

            foreach (var item in cart.CartItems)
            {
                // her bir cartitem içindeki product'ın quantity kadar satılmamış key'i var mı (status == true)

                for (int i = 1; i <= item.Quantity; i++)
                {
                    var orderDetail = new OrderDetails();

                    orderDetail.GameName = item.Product.Game.GameName;
                    orderDetail.ImagePath = item.Product.Game.ImagePath;
                    orderDetail.OrderDiscountId = item.Product.Discounts
                        .FirstOrDefault(x => x.IsValid) == null ? null : item.Product.Discounts.FirstOrDefault(x => x.IsValid).Id;
                    orderDetail.UnitPrice = item.Product.ProductUnitPrice;

                    var key = item.Product.Keys.FirstOrDefault(x => x.Status == true);
                    orderDetail.KeyId = key.Id;
                    key.Status = false;

                    await _keyRepo.UpdateAsync(key);
                    order.OrderDetails.Add(orderDetail);
                }
            }

            return await _orderRepo.AddAsync(order);
        }
    }
}
