using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class ProductViewModelService : IProductViewModelService
    {
        private readonly IProductService _productService;
        private readonly IRepository<Game> _gameRepo;
        private readonly IRepository<Platform> _platformRepo;

        public ProductViewModelService(IProductService productService, IRepository<Game> gameRepo, IRepository<Platform> platformRepo)
        {
            _productService = productService;
            _gameRepo = gameRepo;
            _platformRepo = platformRepo;
        }
        public async Task CreateProductFromViewModelAsync(AdminProductViewModel productViewModel)
        {
            var product = new Product()
            {
                ProductUnitPrice = productViewModel.ProductUnitPrice,
                GameId = productViewModel.GameId,
                PlatformId = productViewModel.PlatformId,
                Game = await _gameRepo.GetByIdAsync(productViewModel.GameId),
                Platform = await _platformRepo.GetByIdAsync(productViewModel.PlatformId)
            };
            await _productService.AddProductAsync(product);
        }

        public async Task<AdminProductViewModel> GetProductEditViewModelAsync(int productId)
        {
            var product = await _productService.GetProductByIdWithAssetsAsync(productId);
            if (product is null)
                throw new ArgumentException("Product can not be found.");

            return new AdminProductViewModel()
            {
                Id = product.Id,
                ProductUnitPrice = product.ProductUnitPrice,
                GameId = product.GameId,
                PlatformId = product.PlatformId,
                IsAvailable = product.IsAvailable,
                IsEditorsChoice = product.IsEditorsChoice,
                AllGames = await GetGamesAsync(),
                AllPlatforms = await GetPlatformsAsync(),
                Game = await _gameRepo.GetByIdAsync(product.GameId),
                Platform = await _platformRepo.GetByIdAsync(product.PlatformId),
                UnitInKeyStock = product.GetUnitStocks()
            };
        }

        public async Task UpdateProductFromViewModelAsync(AdminProductViewModel productViewModel)
        {
            if (productViewModel.Id < 1)
                throw new ArgumentException("Product can not be found.");
            var product = await _productService.GetProductByIdWithAssetsAsync(productViewModel.Id);

            if (product is null)
                throw new ArgumentException("Product can not be found.");

            var oldPlatformName = product.Platform.PlatformName;
            var oldGameName = product.Game.GameName;

            product.GameId = productViewModel.GameId;
            product.PlatformId = productViewModel.PlatformId;
            product.Game = await _gameRepo.GetByIdAsync(productViewModel.GameId);
            product.Platform = await _platformRepo.GetByIdAsync(productViewModel.PlatformId);
            product.IsAvailable = productViewModel.UnitInKeyStock < 1 ? false : productViewModel.IsAvailable;
            product.IsEditorsChoice = productViewModel.IsEditorsChoice;
            product.ProductUnitPrice = productViewModel.ProductUnitPrice;

            await _productService.UpdateProductAsync(product, oldGameName, oldPlatformName);
        }
        public async Task<List<IndexProductViewModel>> GetAllProductsWithViewModel()
        {
            var products = await _productService.GetAllProductsAsync();
            return products.Select(x => new IndexProductViewModel()
            {
                Id = x.Id,
                Discounts = x.Discounts,
                Keys = x.Keys,
                Game = x.Game,
                IsAvailable = x.IsAvailable,
                Platform = x.Platform,
                ProductUnitPrice = x.ProductUnitPrice,
                CurrentDiscountRate = x.GetDiscountRate(),
                UnitInStocks = x.Keys.Where(x => x.Status).Count()
            }).ToList();
        }
        public async Task<List<SelectListItem>> GetGamesAsync()
        {
            var games = await _gameRepo.GetAllAsync();
            return games.Select(x => new SelectListItem() { Text = x.GameName, Value = x.Id.ToString() }).ToList();
        }
        public async Task<List<SelectListItem>> GetPlatformsAsync()
        {
            var platforms = await _platformRepo.GetAllAsync();
            return platforms.Select(x => new SelectListItem() { Text = x.PlatformName, Value = x.Id.ToString() }).ToList();
        }
    }
}
