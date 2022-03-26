using ApplicationCore.Entities;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IPlatformViewModelService
    {
        Task<PlatformEditViewModel> GetPlatformEditViewModelAsync(Platform platform);
        Task CreatePlatformFromViewModelAsync(PlatformViewModel platformViewModel);
        Task UpdatePlatformFromViewModelAsync(PlatformEditViewModel platformEditViewModel);
        Task DeletePlatformThenPictureAsync(int platformId);
    }
}