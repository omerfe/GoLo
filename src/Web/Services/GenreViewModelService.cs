using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class GenreViewModelService : IGenreViewModelService
    {
        public async Task<GenreViewModel> GetGenreViewModelAsync(Genre genre)
        {
            if (genre == null)
                throw new ArgumentException($"Genre can not be found.");
            return new GenreViewModel() { Id = genre.Id, GenreName=genre.GenreName};
        }
    }
}
