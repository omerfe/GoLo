using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilterViewModelService _filterViewModelService;
        private readonly IHomeViewModelService _homeViewModelService;

        public HomeController(ILogger<HomeController> logger, IFilterViewModelService filterViewModelService, IHomeViewModelService homeViewModelService)
        {
            _logger = logger;
            _filterViewModelService = filterViewModelService;
            _homeViewModelService = homeViewModelService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _homeViewModelService.GetHomeViewModelAsync());
        }
        //public async Task<IActionResult> Categories(int? genreId, int? platformId)
        //{
        //    return View(await _filterViewModelService.GetFilterViewModelAsync(genreId, platformId));
        //}

        public async Task<IActionResult> Categories(List<int> genreIds, List<int> platformIds, int? sortItem, string searchText, int p = 1)
        {
            if (p < 1) return BadRequest();
            return View(await _filterViewModelService.GetFilterViewModelAsync(genreIds, platformIds, sortItem, searchText, p));
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
