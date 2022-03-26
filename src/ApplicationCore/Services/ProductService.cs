using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ProductService : IProductService
    {

        private readonly IRepository<Product> _productRepo;

        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            if (productId < 0)
                throw new ArgumentException($"Product with id {productId} can not be found.");

            return await _productRepo.GetByIdAsync(productId);
        }

        public async Task<Product> GetProductByIdWithAssetsAsync(int productId)
        {
            if (productId < 0)
                throw new ArgumentException($"Product with id {productId} can not be found.");
            var spec = new ProductSpecification(productId);
            return await _productRepo.FirstOrDefaultAsync(spec);
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var spec = new ProductSpecification();
            return await _productRepo.GetAllAsync(spec);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            if (product.Game == null)
                throw new ArgumentException("Can not create product without a game.");
            if (product.Platform == null)
                throw new ArgumentException("Can not create product without a platform.");
            var spec = new ProductSpecification(product.Platform.PlatformName, product.Game.GameName);
            var existingProductWithSameName = await _productRepo.FirstOrDefaultAsync(spec);
            if (existingProductWithSameName != null)
                throw new ArgumentException("There is already a Product for this platform and game.");
            return await _productRepo.AddAsync(product);
        }
        public async Task DeleteProductAsync(int productId)
        {
            var spec = new ProductDeleteSpecification(productId);
            var product = await _productRepo.FirstOrDefaultAsync(spec);
            if (product == null)
                throw new ArgumentException($"Product with id {productId} can not be found.");
            if (product.Keys.Any(x => !x.Status))
                throw new ArgumentException($"Product with id {productId} can not be deleted.");
            
            await _productRepo.DeleteAsync(product);
        }
        public async Task UpdateProductAsync(Product product, string oldGameName, string oldPlatformName)
        {
            if (product is null)
                throw new ArgumentException("Product can not be found.");
            var spec = new ProductSpecification(product.Platform.PlatformName, product.Game.GameName);
            var existingProductWithSameName = await _productRepo.FirstOrDefaultAsync(spec);

            // TODO: steam mahmut ve origin mahmut mevcut ===>>> steam mahmutu origin yapınca hata vercez.
            // Durum 1: NP == OP && NG == OG && GPExist ==> var olan kendisidir.
            // Durum 2: NP == OP && NG != OG && GPExist ==> varsa hatalıdır. tick
            // Durum 3: NP != OP && NG == OG && GPExist ==> varsa hatalıdır. tick
            // Durum 4: NP != OP && NG != OG && GPExist ==> varsa hatalıdır. tick
            // Durum 5: NP == OP && NG != OG && GPNOTExist ==> hata yok
            // Durum 6: NP != OP && NG == OG && GPNOTExist ==> hata yok
            // Durum 7: NP != OP && NG != OG && GPNOTExist ==> hata yok
            if (existingProductWithSameName != null && product.Platform.PlatformName == oldPlatformName && product.Game.GameName != oldGameName)
                throw new ArgumentException("There is already a Product for this platform and game.");
            else if (existingProductWithSameName != null && product.Platform.PlatformName != oldPlatformName && product.Game.GameName == oldGameName)
                throw new ArgumentException("There is already a Product for this platform and game.");
            else if (existingProductWithSameName != null && product.Platform.PlatformName != oldPlatformName && product.Game.GameName != oldGameName)
                throw new ArgumentException("There is already a Product for this platform and game.");

            else
                await _productRepo.UpdateAsync(product);

        }
    }
}