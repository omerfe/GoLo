using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;
using Web.Managers;

namespace Web.Services
{
    public class GameViewModelService : IGameViewModelService
    {
        private readonly IGameService _gameService;
        private readonly IRepository<Genre> _genreRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GameViewModelService(IGameService gameService, IRepository<Genre> genreRepo, IWebHostEnvironment webHostEnvironment)
        {
            _gameService = gameService;
            _genreRepo = genreRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task CreateGameFromViewModelAsync(GameViewModel gameViewModel)
        {
            if (!await _gameService.CheckExistingGameithSameNameBeforeAdd(gameViewModel.GameName))
            {
                var imagePath = "";
                try
                {
                    imagePath = gameViewModel.GameImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "games");
                    var game = new Game()
                    {
                        Id = gameViewModel.Id,
                        GameName = gameViewModel.GameName,
                        Description = gameViewModel.Description,
                        Developer = gameViewModel.Developer,
                        GameRequirements = gameViewModel.GameRequirements,
                        MinimumAge = gameViewModel.MinimumAge,
                        Publisher = gameViewModel.Publisher,
                        ReleaseDate = gameViewModel.ReleaseDate,
                        TrailerUrl = gameViewModel.TrailerUrl,
                        ImagePath = imagePath
                    };
                    game.Genres = new List<Genre>();
                    foreach (var genreId in gameViewModel.GenreIds)
                    {
                        var genre = await _genreRepo.GetByIdAsync(genreId);
                        game.Genres.Add(genre);
                    }
                    await _gameService.AddGameAsync(game);
                }
                catch (ArgumentException)
                {
                    if (!string.IsNullOrEmpty(imagePath))
                        FileManager.RemoveImageFromDisk(imagePath, _webHostEnvironment, "games");
                }
            }
            else
                throw new ArgumentException("There is already a Game with same name.");
        }

        public async Task<GameEditViewModel> GetGameEditViewModelAsync(int gameId)
        {
            var game = await _gameService.GetGameByIdWithGenresAsync(gameId);
            if (game is null)
                throw new ArgumentException("Game can not be found.");

            return new GameEditViewModel()
            {
                Id = game.Id,
                GameName = game.GameName,
                Description = game.Description,
                Developer = game.Developer,
                GameRequirements = game.GameRequirements,
                MinimumAge = game.MinimumAge,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                TrailerUrl = game.TrailerUrl,
                ImagePath = game.ImagePath,
                GenreIds = game.Genres.Select(x => x.Id).ToList(),
                AllGenres = await GetGenresAsync()
            };
        }

        public async Task UpdateGameFromViewModelAsync(GameEditViewModel gameEditViewModel)
        {
            if (!await _gameService.CheckExistingGameWithSameNameBeforeUpdate(gameEditViewModel.Id, gameEditViewModel.GameName))
            {
                var imagePath = "";
                try
                {
                    if (gameEditViewModel.GameImage != null)
                    {
                        imagePath = gameEditViewModel.GameImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "games");
                        var oldImagePath = gameEditViewModel.ImagePath;
                        gameEditViewModel.ImagePath = imagePath;
                        await UpdateGamePropertiesAndSaveAsync(gameEditViewModel);
                        FileManager.RemoveImageFromDisk(oldImagePath, _webHostEnvironment, "games");
                    }
                    else
                    {
                        await UpdateGamePropertiesAndSaveAsync(gameEditViewModel);
                    }
                }
                catch (ArgumentException)
                {
                    if (!string.IsNullOrEmpty(imagePath))
                        FileManager.RemoveImageFromDisk(imagePath, _webHostEnvironment, "games");
                }
            }
            else
                throw new ArgumentException("There is already a Game with same name.");
        }

        private async Task UpdateGamePropertiesAndSaveAsync(GameEditViewModel gameEditViewModel)
        {
            if (gameEditViewModel.Id < 0)
                throw new ArgumentException("Game can not be found.");
            var game = await _gameService.GetGameByIdWithGenresAsync(gameEditViewModel.Id);

            if (game is null)
                throw new ArgumentException("Game can not be found.");

            game.GameName = gameEditViewModel.GameName;
            game.Description = gameEditViewModel.Description;
            game.GameRequirements = gameEditViewModel.GameRequirements;
            game.Developer = gameEditViewModel.Developer;
            game.Publisher = gameEditViewModel.Publisher;
            game.TrailerUrl = gameEditViewModel.TrailerUrl;
            game.MinimumAge = gameEditViewModel.MinimumAge;
            game.ImagePath = gameEditViewModel.ImagePath == null ? game.ImagePath : gameEditViewModel.ImagePath;
            game.ReleaseDate = gameEditViewModel.ReleaseDate;

            game.Genres.Clear();
            foreach (var genreId in gameEditViewModel.GenreIds)
            {
                var genre = await _genreRepo.GetByIdAsync(genreId);
                game.Genres.Add(genre);
            }

            await _gameService.UpdateGameAsync(game, gameEditViewModel.GameName);
        }

        public async Task<List<IndexGameViewModel>> GetAllGamesWithViewModel()
        {
            var games = await _gameService.GetAllGamesAsync();
            return games.Select(x => new IndexGameViewModel()
            {
                Id = x.Id,
                GameName = x.GameName,
                Publisher = x.Publisher,
                Developer = x.Developer,
                ReleaseDate = x.ReleaseDate,
                ImagePath = x.ImagePath,
                Genres = x.Genres,
                GenreNames = String.Join(", ", x.Genres.Select(g => g.GenreName))
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetGenresAsync()
        {
            var genres = await _genreRepo.GetAllAsync();
            return genres.Select(x => new SelectListItem() { Text = x.GenreName, Value = x.Id.ToString() }).ToList();
        }

        public async Task DeleteGameThenPictureAsync(int gameId)
        {
            var deletePath = "";
            try
            {
                deletePath = await _gameService.DeleteGameAsync(gameId);
            }
            catch (Exception)
            {
                throw;
            }

            FileManager.RemoveImageFromDisk(deletePath, _webHostEnvironment, "games");
        }
    }
}
