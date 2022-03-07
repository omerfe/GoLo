using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public HomeController(ILogger<HomeController> logger, IFilterViewModelService filterViewModelService)
        {
            _logger = logger;
            _filterViewModelService = filterViewModelService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Categories(int? genreId, int? platformId)
        {
            return View(await _filterViewModelService.GetFilterViewModelAsync(genreId, platformId));
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
