using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGenreService
    {
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task<List<Genre>> GetAllGenresAsync();
        Task<Genre> AddGenreAsync(string genreName);
        Task UpdateGenreAsync(int genreId, string genreName);
        Task DeleteGenreAsync(int genreId);
    }
}
