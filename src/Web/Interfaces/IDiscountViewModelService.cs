using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IDiscountViewModelService
    {
        Task<DiscountViewModel> GetDiscountEditViewModelAsync(int discountId); //EDIT GET
        Task CreateDiscountFromViewModelAsync(DiscountViewModel discountViewModel);  //CREATE POST
        Task UpdateDiscountFromViewModelAsync(DiscountViewModel discountViewModel);   //EDIT POST
        Task<IndexDiscountViewModel> GetAllDiscountsWithViewModel(int productId);       //INDEX GET
    }
}