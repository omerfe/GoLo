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
    public class PlatformService : IPlatformService
    {
        private readonly IRepository<Platform> _platformRepo;

        public PlatformService(IRepository<Platform> platformRepo)
        {
            _platformRepo = platformRepo;
        }
        public async Task<Platform> AddPlatformAsync(string platformName, string logoPath)
        {
            var spec = new PlatformSpecification(platformName);
            var existingPlatformWithSameName = await _platformRepo.FirstOrDefaultAsync(spec);
            if (existingPlatformWithSameName != null)
                throw new ArgumentException("There is already a Platform with same name.");
            var platform = new Platform() { PlatformName = platformName, LogoPath = logoPath };
            return await _platformRepo.AddAsync(platform);
        }

        public async Task<string> DeletePlatformAsync(int platformId)
        {
            var spec = new PlatformSpecification(platformId);
            var platform = await _platformRepo.FirstOrDefaultAsync(spec);
            
            if (platform == null)
                throw new ArgumentException($"Platform with id {platformId} can not be found.");
            var deletePath = platform.LogoPath;

            if (platform.Products.Count > 0)
            {
                throw new ArgumentException($"Platform with id {platformId} can not be deleted.");
            }

            await _platformRepo.DeleteAsync(platform);
            return deletePath;
        }

        public async Task<List<Platform>> GetAllPlatformsAsync()
        {
            return await _platformRepo.GetAllAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(int platformId)
        {
            if (platformId < 0)
                throw new ArgumentException($"Platform with id {platformId} can not be found.");
            return await _platformRepo.GetByIdAsync(platformId);
        }

        public async Task UpdatePlatformAsync(int platformId, string platformName, string logoPath)
        {
            if (platformId < 0)
                throw new ArgumentException($"Platform with id {platformId} can not be found.");
            var platform = await GetPlatformByIdAsync(platformId);
            if (platform == null)
                throw new ArgumentException($"Platform with id {platformId} can not be found.");

            var spec = new PlatformSpecification(platformName);
            var existingPlatformWithSameName = await _platformRepo.FirstOrDefaultAsync(spec);
            if (existingPlatformWithSameName != null && platform.PlatformName != platformName)
                throw new ArgumentException("There is already a Platform with same name.");

            platform.PlatformName = platformName;
            if(logoPath != null)
                platform.LogoPath = logoPath;
            await _platformRepo.UpdateAsync(platform);
        }
    }
}
