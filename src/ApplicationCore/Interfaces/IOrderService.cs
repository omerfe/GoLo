using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int cartId, string email);
        Task<List<Order>> GetAllUserOrdersAsync(string buyerId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrderAsync();
    }
}