using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;

        public HomeViewModelService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var specOnSale = new ProductsOnSaleSpecification();
            var specPreOrders = new ProductsPreOrdersSpecification();
            var specNewRelease = new ProductsNewReleaseSpecification();
            var specEditorsChoice = new ProductsEditorsChoiceSpecification();

            List<Product> productsOnSale = await _productRepo.GetAllAsync(specOnSale);
            List<Product> productsNewReleases = await _productRepo.GetAllAsync(specNewRelease);
            List<Product> productsPreOrders = await _productRepo.GetAllAsync(specPreOrders);
            List<Product> productsEditorsChoice = await _productRepo.GetAllAsync(specEditorsChoice);

            HomeViewModel vm = new HomeViewModel()
            {
                ProductsOnSale = CreateVmPart(productsOnSale),
                ProductsPreOrders = CreateVmPart(productsPreOrders),
                ProductsNewRelease = CreateVmPart(productsNewReleases),
                ProductsEditorsChoice = CreateVmPart(productsEditorsChoice)
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
                DiscountRate = x.Discounts.FirstOrDefault(x => x.IsValid) == null ? 0 : x.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate,
                ReleaseDate = x.Game.ReleaseDate.ToString("dd-MM-yyyy"),
                Genres = string.Join(", ", x.Game.Genres.Select(x => x.GenreName))
            }).ToList();

            return productViewModelList;
        }
    }
}
