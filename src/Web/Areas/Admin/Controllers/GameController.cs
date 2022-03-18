using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;
using Web.Managers;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGameViewModelService _gameViewModelService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Genre> _genreRepo;

        public GameController(IGameService gameService, IGameViewModelService gameViewModelService, IWebHostEnvironment webHostEnvironment, IRepository<Genre> genreRepo)
        {
            _gameService = gameService;
            _gameViewModelService = gameViewModelService;
            _webHostEnvironment = webHostEnvironment;
            _genreRepo = genreRepo;
        }

        // GET: Admin/Game
        public async Task<IActionResult> Index()
        {
            return View(await _gameViewModelService.GetAllGamesWithViewModel());
        }

        // GET: Admin/Game/Create
        public async Task<IActionResult> Create()
        {
            var vm = new GameViewModel() { AllGenres = await _gameViewModelService.GetGenresAsync() };
            return View(vm);
        }

        // POST: Admin/Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //TODO Resim yükle-sil hoş olmadı refactor edilecek
                var imagePath = vm.GameImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "games");
                vm.ImagePath = imagePath;
                try
                {
                    await _gameViewModelService.CreateGameFromViewModelAsync(vm);
                }
                catch (ArgumentException)
                {
                    FileManager.RemoveImageFromDisk(imagePath, _webHostEnvironment, "games");
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGenres = await _gameViewModelService.GetGenresAsync();
            return View(vm);
        }

        // GET: Admin/Game/Edit/5
        public async Task<IActionResult> Edit(int gameId)
        {
            var vm = await _gameViewModelService.GetGameEditViewModelAsync(gameId);
            return View(vm);
        }

        // POST: Admin/Game/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var imagePath = "";
                try
                {
                    if (vm.GameImage != null)
                    {
                        imagePath = vm.GameImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "games");
                        var oldImagePath = vm.ImagePath;
                        vm.ImagePath = imagePath;
                        await _gameViewModelService.UpdateGameFromViewModelAsync(vm);
                        FileManager.RemoveImageFromDisk(oldImagePath, _webHostEnvironment, "games");
                    }
                    else
                    {
                        await _gameViewModelService.UpdateGameFromViewModelAsync(vm);
                    }
                }
                catch (ArgumentException)
                {
                    if (!string.IsNullOrEmpty(imagePath))
                        FileManager.RemoveImageFromDisk(imagePath, _webHostEnvironment, "games");

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGenres = await _gameViewModelService.GetGenresAsync();
            return View(vm);
        }

        // GET: Admin/Game/Delete/5
        public async Task<IActionResult> Delete(int gameId)
        {
            var deletePath = "";
            try
            {
                deletePath = await _gameService.DeleteGameAsync(gameId);
            }
            catch (ArgumentException)
            {
                throw;
            }

            FileManager.RemoveImageFromDisk(deletePath, _webHostEnvironment, "games");

            return RedirectToAction(nameof(Index));
        }
    }
}