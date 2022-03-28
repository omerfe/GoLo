using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;
using Web.Managers;

namespace Web.Services
{
    public class PlatformViewModelService : IPlatformViewModelService
    {
        private readonly IPlatformService _platformService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlatformViewModelService(IPlatformService platformService, IWebHostEnvironment webHostEnvironment)
        {
            _platformService = platformService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PlatformEditViewModel> GetPlatformEditViewModelAsync(Platform platform)
        {
            if (platform == null)
                throw new ArgumentException($"Platform can not be found.");
            return new PlatformEditViewModel() { Id = platform.Id, PlatformName = platform.PlatformName, LogoPath = platform.LogoPath };
        }
        public async Task CreatePlatformFromViewModelAsync(PlatformViewModel platformViewModel)
        {
            if (!await _platformService.CheckExistingPlatformWithSameNameBeforeAdd(platformViewModel.PlatformName))
            {

                var logoPath = platformViewModel.LogoImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "partners");
                try
                {
                    await _platformService.AddPlatformAsync(platformViewModel.PlatformName, logoPath);
                }
                catch (ArgumentException)
                {
                    FileManager.RemoveImageFromDisk(logoPath, _webHostEnvironment, "partners");
                }
            }
            else
                throw new ArgumentException("There is already a Platform with same name.");
        }

        public async Task UpdatePlatformFromViewModelAsync(PlatformEditViewModel platformEditViewModel)
        {
            if (!await _platformService.CheckExistingPlatformWithSameNameBeforeUpdate(platformEditViewModel.Id, platformEditViewModel.PlatformName))
            {
                var logoPath = "";
                try
                {
                    if (platformEditViewModel.LogoImage != null)
                    {

                        logoPath = platformEditViewModel.LogoImage.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment, "partners");
                        await _platformService.UpdatePlatformAsync(platformEditViewModel.Id, platformEditViewModel.PlatformName, logoPath);
                        FileManager.RemoveImageFromDisk(platformEditViewModel.LogoPath, _webHostEnvironment, "partners");
                    }
                    else
                    {
                        await _platformService.UpdatePlatformAsync(platformEditViewModel.Id, platformEditViewModel.PlatformName, platformEditViewModel.LogoPath);
                    }
                }
                catch (ArgumentException)
                {
                    if (!string.IsNullOrEmpty(logoPath))
                        FileManager.RemoveImageFromDisk(logoPath, _webHostEnvironment, "partners");
                }
            }
            else
                throw new ArgumentException("There is already a Platform with same name.");
        }

        public async Task DeletePlatformThenPictureAsync(int platformId)
        {
            //TODO UnitTest
            var deletePath = "";
            try
            {
                deletePath = await _platformService.DeletePlatformAsync(platformId);
            }
            catch (Exception)
            {
                throw;
            }

            FileManager.RemoveImageFromDisk(deletePath, _webHostEnvironment, "partners");
        }
    }
}
