using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class ProductDetailsViewModelService : IProductDetailsViewModelService
    {
        private readonly IRepository<Product> _productRepo;

        public ProductDetailsViewModelService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task<ProductDetailsViewModel> GetProductDetailsViewModelAsync(int productId)
        {
            var specDetail = new ProductsDetailSpecification(productId);
            var product = await _productRepo.FirstOrDefaultAsync(specDetail);
            var specNewRelease = new ProductsNewReleaseSpecification();
            List<Product> productsNewReleases = await _productRepo.GetAllAsync(specNewRelease);
            if (product == null)
                throw new ArgumentException($"Product with the id {productId} can not be found.");
            ProductDetailsViewModel vm = new ProductDetailsViewModel()
            {
                Id = product.Id,
                GameName = product.Game.GameName,
                Description = product.Game.Description,
                Developer = product.Game.Developer,
                Publisher = product.Game.Publisher,
                ReleaseDate = product.Game.ReleaseDate,
                MinimumAge = product.Game.MinimumAge,
                TrailerUrl = product.Game.TrailerUrl,
                ImagePath = product.Game.ImagePath,
                GameRequirements = product.Game.GameRequirements,
                DiscountRate = product.Discounts.FirstOrDefault(x => x.IsValid) == null ? 0 : product.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate,
                UnitPrice = product.ProductUnitPrice,
                PlatformLogo = product.Platform.LogoPath,
                Genres = product.Game.Genres.Select(x => x.GenreName).ToList(),
                ProductsNewRelease = CreateVmPart(productsNewReleases)
            };
            return vm;
        }
        private List<ProductViewModel> CreateVmPart(List<Product> products)
        {
            var productViewModelList = products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                GameName = x.Game.GameName,
                UnitPrice = x.ProductUnitPrice,
                PicturePath = x.Game.ImagePath,
                PlatformLogo = x.Platform.LogoPath,
                DiscountRate = x.Discounts.FirstOrDefault(x => x.IsValid) == null ? 0 : x.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate
            }).ToList();

            return productViewModelList;
        }
    }
}
