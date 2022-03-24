using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;
using Web.Managers;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "admin")]
    public class PlatformController : Controller
    {

        private readonly IPlatformService _platformService;
        private readonly IPlatformViewModelService _platformViewModelService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlatformController(IPlatformService platformService, IPlatformViewModelService platformViewModelService, IWebHostEnvironment webHostEnvironment)
        {
            _platformService = platformService;
            _platformViewModelService = platformViewModelService;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Platforms
        public async Task<IActionResult> Index()
        {
            return View(await _platformService.GetAllPlatformsAsync());
        }

        // GET: Admin/Platforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Platforms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlatformName,LogoImage")] PlatformViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //TODO Resim yükle-sil hoş olmadı refactor edilecek
                var logoPath = vm.LogoImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "partners");
                try
                {
                    await _platformService.AddPlatformAsync(vm.PlatformName, logoPath);
                }
                catch (ArgumentException ex)
                {
                    FileManager.RemoveImageFromDisk(logoPath, _webHostEnvironment, "partners");
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Admin/Platforms/Edit/5
        public async Task<IActionResult> Edit(int platformId)
        {
            Platform platform;
            PlatformEditViewModel vm;
            try
            {
                platform = await _platformService.GetPlatformByIdAsync(platformId);
                vm = await _platformViewModelService.GetPlatformEditViewModelAsync(platform);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Platforms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlatformEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var logoPath = "";
                try
                {
                    if (vm.LogoImage != null)
                    {
                        logoPath = vm.LogoImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "partners");
                        await _platformService.UpdatePlatformAsync(vm.Id, vm.PlatformName, logoPath);
                        FileManager.RemoveImageFromDisk(vm.LogoPath, _webHostEnvironment, "partners");
                    }
                    else
                    {
                        await _platformService.UpdatePlatformAsync(vm.Id, vm.PlatformName, vm.LogoPath);
                    }
                }
                catch (ArgumentException ex)
                {
                    if (!string.IsNullOrEmpty(logoPath))
                        FileManager.RemoveImageFromDisk(logoPath, _webHostEnvironment, "partners");
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Platforms/Delete/5
        public async Task<IActionResult> Delete(int platformId)
        {
            var deletePath = "";
            try
            {
                deletePath = await _platformService.DeletePlatformAsync(platformId);
            }
            catch (ArgumentException ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            FileManager.RemoveImageFromDisk(deletePath, _webHostEnvironment, "partners");

            return RedirectToAction(nameof(Index));
        }
    }
}
