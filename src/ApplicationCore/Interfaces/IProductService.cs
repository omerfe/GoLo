using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductByIdWithAssetsAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product, string oldGameName, string oldPlatformName);
        Task DeleteProductAsync(int productId);
    }
}
