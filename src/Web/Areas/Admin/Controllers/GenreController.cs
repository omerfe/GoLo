using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.Areas.Admin.Models;
using Web.Interfaces;
using Microsoft.AspNetCore.Authorization;

// DİKKAT!!!!! CSHTML'lerin içinde Entity'leri mi kullanmalıyız, ViewModel'leri oluşturup onları mı kullanmalıyız? ViewModel'leri kullanacaksak WEB tarafında IGenreViewModelService adında bir servis açılmalı.
// HomeViewModelService(Index için göstermelik veri gönderiyor) ile CartViewModelService'in(Create-Update-Delete işlerini Application Core'daki CartService yardımı ile yapıyor)  
namespace Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IGenreViewModelService _genreViewModelService;

        public GenreController(IGenreService genreService, IGenreViewModelService genreViewModelService)
        {
            _genreService = genreService;
            _genreViewModelService = genreViewModelService;
        }

        // GET: Admin/Genres
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetAllGenresAsync());
        }

        // GET: Admin/Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreName")] GenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.AddGenreAsync(genre.GenreName);
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(genre);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Admin/Genres/Edit/5
        public async Task<IActionResult> Edit(int genreId)
        {
            Genre genre;
            GenreViewModel vm;
            try
            {
                genre = await _genreService.GetGenreByIdAsync(genreId);
                vm = await _genreViewModelService.GetGenreViewModelAsync(genre);
            }
            catch (ArgumentException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("GenreName,Id")] GenreViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.UpdateGenreAsync(vm.Id, vm.GenreName);
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Genres/Delete/5
        public async Task<IActionResult> Delete(int genreId)
        {
            try
            {
                await _genreService.DeleteGenreAsync(genreId);
            }
            catch (ArgumentException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
