using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepo;

        public GenreService(IRepository<Genre> genreRepo)
        {
            _genreRepo = genreRepo;
        }
        public async Task<Genre> AddGenreAsync(string genreName)
        {
            var genre = new Genre() { GenreName = genreName };
            return await _genreRepo.AddAsync(genre);
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            var spec = new GenreSpecification(genreId);
            var genre = await _genreRepo.FirstOrDefaultAsync(spec);
            if (genre == null)
                throw new ArgumentException($"Genre with id {genreId} can not be found.");

            if (genre.Games.Count > 0)
                throw new ArgumentException($"Genre with id {genreId} can not be deleted.");

            await _genreRepo.DeleteAsync(genre);
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _genreRepo.GetAllAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            if (genreId < 0)
                throw new ArgumentException($"Genre with id {genreId} can not be found.");
            return await _genreRepo.GetByIdAsync(genreId);
        }

        public async Task UpdateGenreAsync(int genreId, string genreName)
        {
            if (genreId < 0)
                throw new ArgumentException($"Genre with id {genreId} can not be found.");
            var genre = await GetGenreByIdAsync(genreId);
            if (genre == null)
                throw new ArgumentException($"Genre with id {genreId} can not be found.");

            genre.GenreName = genreName;
             await _genreRepo.UpdateAsync(genre);
        }
    }
}
