using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = "admin")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IDiscountViewModelService _discountViewModelService;

        public DiscountController(IDiscountService discountService, IDiscountViewModelService discountViewModelService)
        {
            _discountService = discountService;
            _discountViewModelService = discountViewModelService;
        }

        // GET: Admin/Discount/productId
        public async Task<IActionResult> Index(int productId)
        {
            IndexDiscountViewModel vm;
            try
            {
                vm = await _discountViewModelService.GetAllDiscountsWithViewModel(productId);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return NotFound(ex.Message);
            }
            return View(vm);
        }

        //GET: Admin/Discount/Create/productId
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
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
                return RedirectToAction("Index", new { productId = vm.ProductId });
            }
            return View(vm);
        }

        // GET: Admin/Discount/Edit/5
        public async Task<IActionResult> Edit(int discountId)
        {
            DiscountViewModel vm;
            try
            {
                vm = await _discountViewModelService.GetDiscountEditViewModelAsync(discountId);
            }
            catch (ArgumentException ex)
            {
               return NotFound(ex.Message);
            }
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
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
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
            catch (ArgumentException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Index", new { productId = productId });
            }
            return RedirectToAction("Index", new { productId = productId });
        }
    }
}
