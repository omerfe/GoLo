using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using Web.Interfaces;

namespace Web.Services
{
    public class DiscountViewModelService : IDiscountViewModelService
    {
        private readonly IDiscountService _discountService;
        private readonly IRepository<Product> _productRepo;

        public DiscountViewModelService(IDiscountService discountService, IRepository<Product> productRepo)
        {
            _discountService = discountService;
            _productRepo = productRepo;
        }
        public async Task CreateDiscountFromViewModelAsync(DiscountViewModel discountViewModel)
        {
            var discount = new Discount()
            {
                ProductId = discountViewModel.ProductId,
                DiscountRate = discountViewModel.DiscountRate,
                ValidFrom = discountViewModel.ValidFrom,
                ValidUntil = discountViewModel.ValidUntil,
            };
            await _discountService.AddDiscountAsync(discount);
        }
        public async Task<DiscountViewModel> GetDiscountEditViewModelAsync(int discountId)
        {
            var discount = await _discountService.GetDiscountByIdAsync(discountId);
            if (discount is null)
                throw new ArgumentException("Discount can not be found.");

            return new DiscountViewModel()
            {
                Id = discount.Id,
                ProductId = discount.ProductId,
                DiscountRate = discount.DiscountRate,
                ValidFrom = discount.ValidFrom,
                ValidUntil = discount.ValidUntil
            };
        }
        public async Task UpdateDiscountFromViewModelAsync(DiscountViewModel discountViewModel)
        {
            if (discountViewModel.Id < 1)
                throw new ArgumentException("Discount can not be found.");
            var discount = await _discountService.GetDiscountByIdAsync(discountViewModel.Id);

            if (discount is null)
                throw new ArgumentException("Discount can not be found.");

            var oldValidFrom = discount.ValidFrom;
            var oldValidUntil = discount.ValidUntil;

            discount.DiscountRate = discountViewModel.DiscountRate;
            discount.ValidUntil = discountViewModel.ValidUntil;
            discount.ValidFrom = discountViewModel.ValidFrom;

            await _discountService.UpdateDiscountAsync(discount, oldValidFrom, oldValidUntil);
        }
        public async Task<IndexDiscountViewModel> GetAllDiscountsWithViewModel(int productId)
        {
            if (productId < 1)
                throw new ArgumentException("Discount can not be found.");
            var spec = new ProductSpecification(productId);
            var product = await _productRepo.FirstOrDefaultAsync(spec);
            if (product is null)
                throw new ArgumentException("Product can not be found.");

            var discounts = await _discountService.GetAllDiscountsAsync(productId);
            var vm = new IndexDiscountViewModel()
            {
                GameName = product.Game.GameName,
                PlatformName = product.Platform.PlatformName,
                ProductId = productId,
                Discounts = discounts,
            };

            return vm;

        }
    }
}
