using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IRepository<Discount> _discountRepo;
        private readonly IRepository<Product> _productRepo;

        public DiscountService(IRepository<Discount> discountRepo, IRepository<Product> productRepo)
        {
            _discountRepo = discountRepo;
            _productRepo = productRepo;
        }
        public async Task<List<Discount>> GetAllDiscountsAsync(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("Discount can not be found.");
            var spec = new DiscountSpecification(productId);
            return await _discountRepo.GetAllAsync(spec);
        }
        public async Task<Discount> GetDiscountByIdAsync(int discountId)
        {
            if (discountId < 0)
                throw new ArgumentException($"Discount with id {discountId} can not be found.");
            return await _discountRepo.GetByIdAsync(discountId);

        }
        public async Task<Discount> AddDiscountAsync(Discount discount)
        {
            if (discount.ProductId < 0)
                throw new ArgumentException("Can not create discount without a product.");
            var product = await _productRepo.GetByIdAsync(discount.ProductId);
            if (product is null)
                throw new ArgumentException("Can not create discount without a product.");
            var spec = new DiscountSpecification(discount.ValidFrom, discount.ValidUntil);
            var existingDiscount = await _discountRepo.FirstOrDefaultAsync(spec);
            if (existingDiscount is not null)
                throw new ArgumentException("There is already a discount for this product in this timespan.");
            return await _discountRepo.AddAsync(discount);
        }
        public async Task DeleteDiscountAsync(int discountId)
        {
            var discount = await GetDiscountByIdAsync(discountId);
            if (discount == null)
                throw new ArgumentException($"Discount with id {discountId} can not be found.");
            await _discountRepo.DeleteAsync(discount);
        }
        public async Task UpdateDiscountAsync(Discount discount, DateTime oldValidFrom, DateTime oldValidUntil)
        {
            if (discount is null)
                throw new ArgumentException("Discount can not be found.");

            // Durum 1: VF == OVF && VU == OVU && CC == 1 hata yok
            // Durum 2: VF != OVF && VU == OVU && CC == 1 hata yok
            // Durum 3: VF == OVF && VU != OVU && CC == 1 hata yok
            // Durum 4: VF != OVF && VU != OVU ==> 

            var spec = new DiscountSpecification(discount.ValidFrom, discount.ValidUntil);
            var existingDiscounts = await _discountRepo.GetAllAsync(spec);
            var conflictCount = existingDiscounts.Count;

            if (conflictCount > 1)
                throw new ArgumentException("There is already a discount for this product in this timespan.");

            else if (conflictCount == 1)
            {
                if ((discount.ValidFrom == oldValidFrom && discount.ValidUntil == oldValidUntil) ||
                    (discount.ValidFrom != oldValidFrom && discount.ValidUntil == oldValidUntil) ||
                    (discount.ValidFrom == oldValidFrom && discount.ValidUntil != oldValidUntil))
                {
                    await _discountRepo.UpdateAsync(discount);
                }
                else if (discount.ValidFrom != oldValidFrom && discount.ValidUntil != oldValidUntil)
                {
                    if (discount.ValidFrom > oldValidFrom && discount.ValidUntil > oldValidUntil)
                    {
                        if (discount.ValidFrom <= oldValidUntil)
                            await _discountRepo.UpdateAsync(discount);
                        else
                            throw new ArgumentException("There is already a discount for this product in this timespan.");
                    }
                    else if (discount.ValidFrom < oldValidFrom && discount.ValidUntil < oldValidUntil)
                    {
                        if (discount.ValidUntil >= oldValidFrom)
                            await _discountRepo.UpdateAsync(discount);
                        else
                            throw new ArgumentException("There is already a discount for this product in this timespan.");
                    }
                    else
                        await _discountRepo.UpdateAsync(discount);
                }
                else
                    throw new ArgumentException("Unknown exception");
            }

            else
                await _discountRepo.UpdateAsync(discount);
        }
    }
}