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
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KeyController : Controller
    {
        private readonly IKeyService _keyService;
        private readonly IKeyViewModelService _keyViewModelService;

        public KeyController(IKeyService keyService, IKeyViewModelService keyViewModelService)
        {
            _keyService = keyService;
            _keyViewModelService = keyViewModelService;
        }

        // GET: Admin/Key
        public async Task<IActionResult> Index(int productId)
        {
            return View(await _keyViewModelService.GetAllKeysWithViewModel(productId));
        }

        //GET: Admin/Key/Create
        public async Task<IActionResult> Create(int productId)
        {
            var vm = new KeyViewModel() { ProductId = productId };
            return View(vm);
        }

        // POST: Admin/Key/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KeyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _keyViewModelService.CreateKeyFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction("Index", new { productId = vm.ProductId });
            }


            return View(vm);
        }

        // GET: Admin/Key/Edit/5
        public async Task<IActionResult> Edit(int keyId)
        {
            var vm = await _keyViewModelService.GetKeyEditViewModelAsync(keyId);
            return View(vm);
        }

        // POST: Admin/Key/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KeyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _keyViewModelService.UpdateKeyFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    throw;
                }
                return RedirectToAction("Index", new { productId = vm.ProductId });
            }

            return View(vm);
        }

        // GET: Admin/Key/Delete/5
        public async Task<IActionResult> Delete(int keyId, int productId)
        {
            try
            {
                await _keyService.DeleteKeyAsync(keyId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            return RedirectToAction("Index", new { productId = productId });
        }
    }
}
