using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Infrastructure.Data;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductViewModelService _productViewModelService;

        public ProductController(IProductService productService, IProductViewModelService productViewModelService)
        {
            _productService = productService;
            _productViewModelService = productViewModelService;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            return View(await _productViewModelService.GetAllProductsWithViewModel());
        }

        //GET: Admin/Product/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AdminProductViewModel() { AllGames = await _productViewModelService.GetGamesAsync() };
            vm.AllPlatforms = await _productViewModelService.GetPlatformsAsync();

            return View(vm);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productViewModelService.CreateProductFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGames = await _productViewModelService.GetGamesAsync();
            vm.AllPlatforms = await _productViewModelService.GetPlatformsAsync();

            return View(vm);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int productId)
        {
            var vm = await _productViewModelService.GetProductEditViewModelAsync(productId);
            return View(vm);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productViewModelService.UpdateProductFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGames = await _productViewModelService.GetGamesAsync();
            vm.AllPlatforms = await _productViewModelService.GetPlatformsAsync();
            return View(vm);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                await _productService.DeleteProductAsync(productId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
