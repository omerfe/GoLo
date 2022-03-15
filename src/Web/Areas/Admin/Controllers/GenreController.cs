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

// DİKKAT!!!!! CSHTML'lerin içinde Entity'leri mi kullanmalıyız, ViewModel'leri oluşturup onları mı kullanmalıyız? ViewModel'leri kullanacaksak WEB tarafında IGenreViewModelService adında bir servis açılmalı.
// HomeViewModelService(Index için göstermelik veri gönderiyor) ile CartViewModelService'in(Create-Update-Delete işlerini Application Core'daki CartService yardımı ile yapıyor)  
namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly GoloContext _context;
        private readonly IGenreService _genreService;

        public GenreController(GoloContext context, IGenreService genreService)
        {
            _context = context;
            _genreService = genreService;
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
        public async Task<IActionResult> Create([Bind("GenreName,Id")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreService.AddGenreAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Admin/Genres/Edit/5
        public async Task<IActionResult> Edit(int genreId)
        {
            var genre = await _genreService.GetGenreByIdAsync(genreId);
            
            return View(genre);
        }

        // POST: Admin/Genres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("GenreName,Id")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.UpdateGenreAsync(genre.Id, genre.GenreName);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Admin/Genres/Delete/5
        public async Task<IActionResult> Delete(int genreId)
        {
            await _genreService.DeleteGenreAsync(genreId);

            return RedirectToAction(nameof(Index));
        }
    }
}
