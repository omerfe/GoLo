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
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IGameViewModelService _gameViewModelService;

        public GameController(IGameService gameService, IGameViewModelService gameViewModelService)
        {
            _gameService = gameService;
            _gameViewModelService = gameViewModelService;
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
                try
                {
                    await _gameViewModelService.CreateGameFromViewModelAsync(vm);
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    vm.AllGenres = await _gameViewModelService.GetGenresAsync();
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGenres = await _gameViewModelService.GetGenresAsync();
            return View(vm);
        }

        // GET: Admin/Game/Edit/5
        public async Task<IActionResult> Edit(int gameId)
        {
            GameEditViewModel vm;
            try
            {
                vm = await _gameViewModelService.GetGameEditViewModelAsync(gameId);
            }
            catch (ArgumentException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // POST: Admin/Game/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GameEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _gameViewModelService.UpdateGameFromViewModelAsync(vm);
                }
                catch (ArgumentException ex)
                {
                    ViewBag.Message = ex.Message;
                    vm.AllGenres = await _gameViewModelService.GetGenresAsync();
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            vm.AllGenres = await _gameViewModelService.GetGenresAsync();
            return View(vm);
        }

        // GET: Admin/Game/Delete/5
        public async Task<IActionResult> Delete(int gameId)
        {
            try
            {
                await _gameViewModelService.DeleteGameThenPictureAsync(gameId);
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