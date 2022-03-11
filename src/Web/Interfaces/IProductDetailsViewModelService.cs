using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Interfaces
{
    public interface IProductDetailsViewModelService
    {
        Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int productId);
    }
}
