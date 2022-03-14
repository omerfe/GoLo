using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartViewModelService _cartViewModelService;
        private readonly ICartService _cartService;

        public CartController(ICartViewModelService cartViewModelService, ICartService cartService)
        {
            _cartViewModelService = cartViewModelService;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _cartViewModelService.GetCartViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var cart = await _cartViewModelService.AddToCartAsync(productId, quantity);
            return Json(new NavCartViewModel() { TotalItemsCount = cart.TotalItemsCount });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([ModelBinder(Name = "quantities")] Dictionary<int, int> quantities)
        {
            await _cartViewModelService.UpdateCartAsync(quantities);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Empty()
        {
            var cart = await _cartViewModelService.GetOrCreateCartAsync();
            await _cartService.DeleteCartAsync(cart.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var cart = await _cartViewModelService.GetOrCreateCartAsync();
            await _cartService.DeleteCartItemAsync(cart.Id, cartItemId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //payment received..
                var result = await _cartViewModelService.CompleteCheckoutAsync(vm.Email);
                if (result is null)
                {
                    ViewBag.OutOfStock = "Some of the items you've selected has changed due to their stock status.";
                    return View(vm);
                }
                return RedirectToAction("OrderComplete", result);
            }

            return View(vm);
        }

        public async Task<IActionResult> OrderComplete(OrderCompleteViewModel vm)
        {
            return View(vm);
        }
    }
}
