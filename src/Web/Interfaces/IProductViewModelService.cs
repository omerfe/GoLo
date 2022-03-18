using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;

namespace Web.Interfaces
{
   public interface IProductViewModelService
    {
        Task<AdminProductViewModel> GetProductEditViewModelAsync(int productId); //EDIT GET
        Task CreateProductFromViewModelAsync(AdminProductViewModel productViewModel);  //CREATE POST
        Task UpdateProductFromViewModelAsync(AdminProductViewModel productViewModel);   //EDIT POST
        Task<List<IndexProductViewModel>> GetAllProductsWithViewModel();       //INDEX GET
        Task<List<SelectListItem>> GetGamesAsync();       
        Task<List<SelectListItem>> GetPlatformsAsync();
    }
}
