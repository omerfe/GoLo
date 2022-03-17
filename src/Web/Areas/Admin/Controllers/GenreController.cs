using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.Areas.Admin.Models;
using Web.Interfaces;

// DİKKAT!!!!! CSHTML'lerin içinde Entity'leri mi kullanmalıyız, ViewModel'leri oluşturup onları mı kullanmalıyız? ViewModel'leri kullanacaksak WEB tarafında IGenreViewModelService adında bir servis açılmalı.
// HomeViewModelService(Index için göstermelik veri gönderiyor) ile CartViewModelService'in(Create-Update-Delete işlerini Application Core'daki CartService yardımı ile yapıyor)  
namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                await _genreService.AddGenreAsync(genre.GenreName);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Admin/Genres/Edit/5
        public async Task<IActionResult> Edit(int genreId)
        {
            var genre = await _genreService.GetGenreByIdAsync(genreId);
            var vm = await _genreViewModelService.GetGenreViewModelAsync(genre);
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
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Genres/Delete/5
        public async Task<IActionResult> Delete(int genreId)
        {
            await _genreService.DeleteGenreAsync(genreId);

            return RedirectToAction(nameof(Index));
        }
    }
}
