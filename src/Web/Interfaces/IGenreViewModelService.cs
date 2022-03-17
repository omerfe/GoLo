using ApplicationCore.Entities;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IGenreViewModelService
    {
        Task<GenreViewModel> GetGenreViewModelAsync(Genre genre);
    }
}
