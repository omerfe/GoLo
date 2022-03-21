using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class KeyViewModelService : IKeyViewModelService
    {
        private readonly IKeyService _keyService;
        private readonly IRepository<Game> _gameRepo;
        private readonly IRepository<Platform> _platformRepo;
        private readonly IRepository<Product> _productRepo;

        public KeyViewModelService(IKeyService keyService, IRepository<Game> gameRepo, IRepository<Platform> platformRepo, IRepository<Product> productRepo)
        {
            _keyService = keyService;
            _gameRepo = gameRepo;
            _platformRepo = platformRepo;
            _productRepo = productRepo;
        }
        public async Task CreateKeyFromViewModelAsync(KeyViewModel keyViewModel)
        {
            var key = new Key()
            {
                ProductId = keyViewModel.ProductId,
                KeyCode = keyViewModel.KeyCode
            };
            await _keyService.AddKeyAsync(key);
        }
        public async Task<KeyViewModel> GetKeyEditViewModelAsync(int keyId)
        {
            var key = await _keyService.GetKeyByIdAsync(keyId);
            if (key is null)
                throw new ArgumentException("Key can not be found.");

            return new KeyViewModel()
            {
                Id = key.Id,
                ProductId = key.ProductId,
                KeyCode = key.KeyCode
            };
        }
        public async Task UpdateKeyFromViewModelAsync(KeyViewModel keyViewModel)
        {
            if (keyViewModel.Id < 0)
                throw new ArgumentException("Key can not be found.");
            var key = await _keyService.GetKeyByIdAsync(keyViewModel.Id);

            if (key is null)
                throw new ArgumentException("Key can not be found.");

            var oldKeyCode = key.KeyCode;

            key.KeyCode = keyViewModel.KeyCode;

            await _keyService.UpdateKeyAsync(key, oldKeyCode);
        }
        public async Task<IndexKeyViewModel> GetAllKeysWithViewModel(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("Key can not be found.");
            var spec = new ProductSpecification(productId);
            var product = await _productRepo.FirstOrDefaultAsync(spec);
            if (product is null)
                throw new ArgumentException("Product can not be found.");

            var keys = await _keyService.GetAllKeysAsync(productId);
            var vm = new IndexKeyViewModel()
            {
                GameName = product.Game.GameName,
                PlatformName = product.Platform.PlatformName,
                ProductId = productId,
                Keys = keys,
            };

            return vm;
        }
    }
}
