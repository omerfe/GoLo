using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class OrderViewModelService : IOrderViewModelService
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IOrderService _orderService;

        public OrderViewModelService(IRepository<Order> orderRepo, IOrderService orderService)
        {
            _orderRepo = orderRepo;
            _orderService = orderService;
        }
        public async Task<List<OrderViewModel>> GetAllGamesWithViewModel()
        {
            var orders = await _orderService.GetAllOrderAsync();

            return orders.Select(x => new OrderViewModel() 
            {
                Id = x.Id,
                BuyerId = x.BuyerId,
                OrderDate = x.OrderDate,
                OrderDetails = x.OrderDetails
            }).ToList();
        }

        public async Task<OrderViewModel> GetOrderWithViewModels(int orderId)
        {
            if (orderId < 1)
                throw new ArgumentException("Order can not be found!");

            var spec = new OrderSpecification(orderId);
            var order = await _orderRepo.FirstOrDefaultAsync(spec);

            if (order == null)
                throw new ArgumentException("Order can not be found!");

            return new OrderViewModel()
            {
                Id =order.Id,
                BuyerId = order.BuyerId,
                OrderDate = order.OrderDate,
                OrderDetails = order.OrderDetails
            };
        }
    }
}
