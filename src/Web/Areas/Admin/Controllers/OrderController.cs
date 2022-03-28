using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderViewModelService _orderViewModelService;

        public OrderController(IOrderService orderService, IOrderViewModelService orderViewModelService)
        {
            _orderService = orderService;
            _orderViewModelService = orderViewModelService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _orderViewModelService.GetAllGamesWithViewModel());
        }
        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int orderId)
        {
            OrderViewModel vm;
            try
            {
                vm = await _orderViewModelService.GetOrderWithViewModels(orderId);
            }
            catch (ArgumentException ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }
    }
}
