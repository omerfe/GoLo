using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.ViewComponents
{
    public class CheckoutCartViewComponent : ViewComponent
    {
        private readonly ICartViewModelService _cartViewModelService;

        public CheckoutCartViewComponent(ICartViewModelService cartViewModelService)
        {
            _cartViewModelService = cartViewModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _cartViewModelService.GetCartViewModelAsync());
        }
    }
}
