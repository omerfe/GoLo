using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPlatformService
    {
        Task<Platform> GetPlatformByIdAsync(int platformId);
        Task<List<Platform>> GetAllPlatformsAsync();
        Task<Platform> AddPlatformAsync(string platformName, string logoPath);
        Task UpdatePlatformAsync(int platformId, string platformName, string logoPath);
        Task<string> DeletePlatformAsync(int platformId);
        Task<bool> CheckExistingPlatformWithSameNameBeforeAdd(string platformName);
        Task<bool> CheckExistingPlatformWithSameNameBeforeUpdate(int platformId, string newPlatformName);
    }
}
