using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IOrderViewModelService
    {
        Task<List<OrderViewModel>> GetAllGamesWithViewModel();
        Task<OrderViewModel> GetOrderWithViewModels(int orderId);
    }
}
