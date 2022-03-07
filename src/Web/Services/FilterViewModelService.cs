using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class FilterViewModelService : IFilterViewModelService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Genre> _genreRepo;
        private readonly IRepository<Platform> _platformRepo;
        private readonly IRepository<Discount> _discountRepo;

        public FilterViewModelService(IRepository<Product> productRepo, IRepository<Genre> genreRepo, IRepository<Platform> platformRepo, IRepository<Discount> discountRepo)
        {
            _productRepo = productRepo;
            _genreRepo = genreRepo;
            _platformRepo = platformRepo;
            _discountRepo = discountRepo;
        }

        public async Task<FilterViewModel> GetFilterViewModelAsync(int? genreId, int? platformId)
        {
            var specProducts = new ProductsFilterSpecification(genreId, platformId);
            List<Product> products = await _productRepo.GetAllAsync(specProducts);

            List<Genre> genres = await _genreRepo.GetAllAsync();
            List<Platform> platforms = await _platformRepo.GetAllAsync();

            //var specDiscounts = await _discountRepo.GetAllAsync(new DiscountSpesification(x.Id))[0]

            var discountRate = await _discountRepo.GetAllAsync();

            FilterViewModel vm = new FilterViewModel()
            {
                Products = products.Select(x => new ProductViewModel()
                {

                    Id = x.Id,
                    GameName = x.Game.GameName,
                    UnitPrice = x.ProductUnitPrice,
                    PicturePath = x.Game.ImagePath,
                    PlatformLogo = x.Platform.LogoPath,
                    DiscountRate = x.Discounts.FirstOrDefault(x => x.IsValid) == null ? 0 : x.Discounts.FirstOrDefault(x => x.IsValid).DiscountRate
                }).ToList(),
                Genres = genres.Select(x => new SelectListItem(x.GenreName, x.Id.ToString())).ToList(),
                Platforms = platforms.Select(x => new SelectListItem(x.PlatformName, x.Id.ToString())).ToList(),
                GenreId = genreId,
                PlatformId = platformId
            };

            return vm;
        }
    }
}
