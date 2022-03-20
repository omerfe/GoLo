using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IDiscountViewModelService _discountViewModelService;

        public DiscountController(IDiscountService discountService, IDiscountViewModelService discountViewModelService)
        {
            _discountService = discountService;
            _discountViewModelService = discountViewModelService;
        }

        // GET: Admin/Discount
        public async Task<IActionResult> Index(int productId)
        {
            return View(await _discountViewModelService.GetAllDiscountsWithViewModel(productId));
        }

        //GET: Admin/Discount/Create
        public async Task<IActionResult> Create(int productId)
        {
            var vm = new DiscountViewModel() { ProductId = productId };
            return View(vm);
        }

        // POST: Admin/Discount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _discountViewModelService.CreateDiscountFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction("Index", new { productId = vm.ProductId });
            }



            return View(vm);
        }

        // GET: Admin/Discount/Edit/5
        public async Task<IActionResult> Edit(int discountId)
        {
            var vm = await _discountViewModelService.GetDiscountEditViewModelAsync(discountId);
            return View(vm);
        }

        // POST: Admin/Discount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiscountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _discountViewModelService.UpdateDiscountFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction("Index", new { productId = vm.ProductId });
            }

            return View(vm);
        }

        // GET: Admin/Discount/Delete/5
        public async Task<IActionResult> Delete(int discountId, int productId)
        {
            try
            {
                await _discountService.DeleteDiscountAsync(discountId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            return RedirectToAction("Index", new { productId = productId });
        }
    }
}
