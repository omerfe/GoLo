using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IHomeViewModelService
    {
        Task<HomeViewModel> GetHomeViewModelAsync();
    }
}
