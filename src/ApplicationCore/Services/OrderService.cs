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
        private readonly IRepository<Product> _productRepo;

        public OrderService(IRepository<Cart> cartRepo, IRepository<Order> orderRepo, IRepository<Key> keyRepo, IRepository<Product> productRepo)
        {
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _keyRepo = keyRepo;
            _productRepo = productRepo;
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
                OrderDetails = new List<OrderDetail>()
            };

            var outOfStock = 0;
            var cartItems = cart.CartItems;
            var cartItemIdsForRemove = new List<int>();
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
                    cartItemIdsForRemove.Add(cartItems[i].Id);
            }
            if (cartItemIdsForRemove.Count > 0)
            {
                for (int i = 0; i < cartItemIdsForRemove.Count; i++)
                {
                    cart.CartItems.Remove(cartItems.FirstOrDefault(x => x.Id == cartItemIdsForRemove[i]));
                }
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
                    var orderDetail = new OrderDetail();

                    orderDetail.GameName = item.Product.Game.GameName;
                    orderDetail.ImagePath = item.Product.Game.ImagePath;
                    orderDetail.OrderDiscountId = item.Product.Discounts
                        .FirstOrDefault(x => x.IsValid) == null ? null : item.Product.Discounts.FirstOrDefault(x => x.IsValid).Id;
                    orderDetail.UnitPrice = item.Product.Discounts.FirstOrDefault(x => x.IsValid) == null ? item.Product.ProductUnitPrice :
                        (item.Product.ProductUnitPrice * (100 - item.Product.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate) / 100);
                    var key = item.Product.Keys.FirstOrDefault(x => x.Status == true);
                    orderDetail.KeyId = key.Id;
                    key.Status = false;

                    await _keyRepo.UpdateAsync(key);
                    order.OrderDetails.Add(orderDetail);
                    if (item.Product.Keys.Where(x => x.Status).ToList().Count() < 1)
                    {
                        item.Product.IsAvailable = false;
                        await _productRepo.UpdateAsync(item.Product);
                    }
                }
            }
            return await _orderRepo.AddAsync(order);
        }

        public async Task<List<Order>> GetAllOrderAsync()
        {
            var spec = new OrderSpecification();
            return await _orderRepo.GetAllAsync(spec);
        }

        public async Task<List<Order>> GetAllUserOrdersAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
                throw new ArgumentException("Buyer can not be found!");

            var spec = new OrderSpecification(buyerId);
            var buyerOrders = await _orderRepo.GetAllAsync(spec);

            return buyerOrders;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var spec = new OrderSpecification(orderId);
            var order = await _orderRepo.FirstOrDefaultAsync(spec);

            return order;
        }
    }
}
