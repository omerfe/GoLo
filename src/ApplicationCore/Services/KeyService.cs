using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class KeyService : IKeyService
    {
        private readonly IRepository<Key> _keyRepo;
        private readonly IRepository<Product> _productRepo;

        public KeyService(IRepository<Key> keyRepo, IRepository<Product> productRepo)
        {
            _keyRepo = keyRepo;
            _productRepo = productRepo;
        }
        public async Task<List<Key>> GetAllKeysAsync(int productId)
        {
            if (productId < 1)
                throw new ArgumentException("Key can not be found.");
            var spec = new KeySpecification(productId);
            return await _keyRepo.GetAllAsync(spec);
        }
        public async Task<Key> GetKeyByIdAsync(int keyId)
        {
            if (keyId < 1)
                throw new ArgumentException($"Key with id {keyId} can not be found.");
            return await _keyRepo.GetByIdAsync(keyId);

        }
        public async Task<Key> AddKeyAsync(Key key)
        {
            if (key.ProductId < 1)
                throw new ArgumentException("Can not create key without a product.");
            var product = await _productRepo.GetByIdAsync(key.ProductId);
            if (product is null)
                throw new ArgumentException("Can not create key without a product.");
            var spec = new KeySpecification(key.KeyCode);
            var existingKey = await _keyRepo.FirstOrDefaultAsync(spec);
            if (existingKey is not null)
                throw new ArgumentException("There is already a key with this keycode.");
            return await _keyRepo.AddAsync(key);
        }
        public async Task DeleteKeyAsync(int keyId)
        {
            var key = await GetKeyByIdAsync(keyId);
            if (key == null)
                throw new ArgumentException($"Key with id {keyId} can not be found.");
            if (!key.Status)
                throw new ArgumentException($"Key with id {keyId} can not be deleted.");
            await _keyRepo.DeleteAsync(key);
        }
        public async Task UpdateKeyAsync(Key key, Guid oldKeyCode)
        {
            if (key is null)
                throw new ArgumentException("Key can not be found.");
            var spec = new KeySpecification(key.KeyCode);
            var existingKeyWithSameKeyCode = await _keyRepo.FirstOrDefaultAsync(spec);
            if (existingKeyWithSameKeyCode != null && key.KeyCode != oldKeyCode)
                throw new ArgumentException("There is already a key with this keycode.");
            await _keyRepo.UpdateAsync(key);
        }
    }
}
