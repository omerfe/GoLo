using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
    public interface IKeyViewModelService
    {
        Task<KeyViewModel> GetKeyEditViewModelAsync(int keyId); //EDIT GET
        Task CreateKeyFromViewModelAsync(KeyViewModel keyViewModel);  //CREATE POST
        Task UpdateKeyFromViewModelAsync(KeyViewModel keyViewModel);   //EDIT POST
        Task<IndexKeyViewModel> GetAllKeysWithViewModel(int productId);       //INDEX GET
    }
}
