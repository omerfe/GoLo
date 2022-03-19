using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IDiscountService
    {
        Task<Discount> GetDiscountByIdAsync(int discountId);
        Task<List<Discount>> GetAllDiscountsAsync(int productId);
        Task<Discount> AddDiscountAsync(Discount discount);
        Task UpdateDiscountAsync(Discount discount, DateTime oldValidFrom, DateTime oldValidUntil);
        Task DeleteDiscountAsync(int discountId);
    }
}