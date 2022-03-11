using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class CartTransferMiddleware
    {
        private readonly RequestDelegate _next;
        public CartTransferMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICartService cartService)
        {
            if (context.User.Identity.IsAuthenticated && context.Request.Cookies.ContainsKey(Constants.CART_COOKIENAME))
            {
                var anonId = context.Request.Cookies[Constants.CART_COOKIENAME];
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await cartService.TransferCartAsync(anonId, userId);
                context.Response.Cookies.Delete(Constants.CART_COOKIENAME);
            }
            await _next(context);
        }
    }
}
