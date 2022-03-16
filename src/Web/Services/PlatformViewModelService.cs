using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class PlatformViewModelService : IPlatformViewModelService
    {
        public async Task<PlatformEditViewModel> GetPlatformEditViewModelAsync(Platform platform)
        {
            if (platform == null)
                throw new ArgumentException($"Platform can not be found.");
            return new PlatformEditViewModel() { Id = platform.Id, PlatformName = platform.PlatformName, LogoPath = platform.LogoPath };
        }
    }
}
