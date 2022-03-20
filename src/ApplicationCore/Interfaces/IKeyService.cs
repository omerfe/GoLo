using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IKeyService
    {
        Task<Key> GetKeyByIdAsync(int keyId);
        Task<List<Key>> GetAllKeysAsync(int productId);
        Task<Key> AddKeyAsync(Key key);
        Task UpdateKeyAsync(Key key, Guid oldKeyCode);
        Task DeleteKeyAsync(int keyId);
    }
}
