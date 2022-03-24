using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public partial class OrderModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;

        public OrderModel(UserManager<ApplicationUser> userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public List<OrderViewModel> OrderViewModels { get; set; }
        }

        public class OrderViewModel
        {
            public int OrderId { get; set; }
            public string BuyerId { get; set; }
            public string OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
            public int KeyQuantity { get; set; }
            public List<OrderDetailsModel> OrderDetails { get; set; } = new List<OrderDetailsModel>();
        }

        public class OrderDetailsModel
        {
            public string Platform { get; set; }
            public string Game { get; set; }
            public decimal Price { get; set; }
            public Guid KeyCode { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var buyerOrders = await _orderService.GetAllUserOrdersAsync(userId);
            Input = new InputModel
            {
                OrderViewModels = buyerOrders.Select(x =>
                new OrderViewModel()
                {
                    OrderId = x.Id,
                    BuyerId = x.BuyerId,
                    OrderDate = x.OrderDate.ToString("dd-MM-yyyy"),
                    TotalPrice = x.OrderDetails.Sum(x => x.UnitPrice),
                    KeyQuantity = x.OrderDetails.Count(),
                    OrderDetails = x.OrderDetails.Select(y =>
                        new OrderDetailsModel()
                        {
                            Game = y.GameName,
                            Platform = y.Key.Product.Platform.PlatformName,
                            Price = y.UnitPrice,
                            KeyCode = y.Key.KeyCode
                        }).ToList()
                }).ToList()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            try
            {
                await LoadAsync(user);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    await LoadAsync(user);
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
                return Page();
            }

            return RedirectToPage();
        }
    }
}
