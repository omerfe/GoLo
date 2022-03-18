using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IGameViewModelService
    {
        Task<GameEditViewModel> GetGameEditViewModelAsync(int gameId);
        Task CreateGameFromViewModelAsync(GameViewModel gameViewModel);
        Task UpdateGameFromViewModelAsync(GameEditViewModel gameEditViewModel);
        Task<List<IndexGameViewModel>> GetAllGamesWithViewModel();
        Task<List<SelectListItem>> GetGenresAsync();
    }
}