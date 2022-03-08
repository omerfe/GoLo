using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IFilterViewModelService
    {
        //Task<FilterViewModel> GetFilterViewModelAsync(int? genreId, int? platformId);
        Task<FilterViewModel> GetFilterViewModelAsync(List<int> genreIds, List<int> platformIds, int page);

    }
}
