using FluentValidation;

namespace Web.Areas.Admin.Models.Validators
{
    public class AdminProductViewModelValidator : AbstractValidator<AdminProductViewModel>
    {
        public AdminProductViewModelValidator()
        {
            RuleFor(x => x.ProductUnitPrice)
                .ScalePrecision(2, 18).WithMessage("Only two decimal points allowed.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            // TODO: Input="Text" Decimal points comma or dot validation.
        }
    }
}