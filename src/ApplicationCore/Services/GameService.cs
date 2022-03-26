using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepo;

        public GameService(IRepository<Game> gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            if (gameId < 0)
                throw new ArgumentException($"Game with id {gameId} can not be found.");

            return await _gameRepo.GetByIdAsync(gameId);
        }

        public async Task<Game> GetGameByIdWithGenresAsync(int gameId)
        {
            if (gameId < 0)
                throw new ArgumentException($"Game with id {gameId} can not be found.");
            var spec = new GameSpecification(gameId);
            return await _gameRepo.FirstOrDefaultAsync(spec);
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            var spec = new GameSpecification();
            return await _gameRepo.GetAllAsync(spec);
        }

        public async Task<Game> AddGameAsync(Game game)
        {
            if (game == null)
                throw new ArgumentException($"Game can not be found.");
            if (await CheckExistingGameithSameNameBeforeAdd(game.GameName))
                throw new ArgumentException("There is already a Game with same name.");
            return await _gameRepo.AddAsync(game);
        }

        public async Task<string> DeleteGameAsync(int gameId)
        {
            var spec = new GameSpecification(gameId);
            var game = await _gameRepo.FirstOrDefaultAsync(spec);
            if (game == null)
                throw new ArgumentException($"Game with id {gameId} can not be found.");

            if (game.Products.Count > 0)
                throw new ArgumentException($"Game with id {gameId} can not be deleted.");

            var deletePath = game.ImagePath;



            await _gameRepo.DeleteAsync(game);
            return deletePath;
        }

        public async Task UpdateGameAsync(Game game, string newGameName)
        {
            if (game is null)
                throw new ArgumentException($"Game can not be found.");
            //--Platform'dan farklı olarak Yeni oyun ismini web tarafında game'e yükleyip gönderdiğimiz için ve ef bu entity'i takip ettiği için alttaki metodu çağıramıyoruz.

            //var spec = new GameSpecification(game.GameName);
            //var existingGameWithSameName = await _gameRepo.FirstOrDefaultAsync(spec);
            //if (existingGameWithSameName != null && game.GameName != oldGameName)
            //    throw new ArgumentException("There is already a Game with same name.");
            if (await CheckExistingGameWithSameNameBeforeUpdate(game.Id, newGameName))
                throw new ArgumentException("There is already a Game with same name.");
            //--

            await _gameRepo.UpdateAsync(game);
        }

        public async Task<bool> CheckExistingGameithSameNameBeforeAdd(string gameName)
        {
            var spec = new GameSpecification(gameName);
            var existingGameWithSameName = await _gameRepo.FirstOrDefaultAsync(spec);
            if (existingGameWithSameName != null)
                return true;
            return false;
        }

        public async Task<bool> CheckExistingGameWithSameNameBeforeUpdate(int gameId, string newGameName)
        {
            if (gameId < 0)
                return true;
            var game = await GetGameByIdAsync(gameId);
            if (game == null)
                return true;
            var spec = new GameSpecification(newGameName);
            var existingGameWithSameName = await _gameRepo.FirstOrDefaultAsync(spec);
            if (existingGameWithSameName != null && game.GameName != newGameName)
                return true;
            return false;
        }
    }
}