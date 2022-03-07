using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IFilterViewModelService
    {
        Task<FilterViewModel> GetFilterViewModelAsync(int? genreId, int? platformId);

    }
}
