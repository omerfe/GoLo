using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class DiscountViewModelValidator : AbstractValidator<DiscountViewModel>
    {
        public DiscountViewModelValidator()
        {
            RuleFor(x => x.DiscountRate).InclusiveBetween(5, 100).WithMessage("Discount rate must be between 5 and 100.").Must(dr => dr % 5 == 0).WithMessage("Discount rate must be a multiple of 5.").NotEmpty().WithMessage("This field is required.");
            RuleFor(x => x.ValidFrom).LessThanOrEqualTo(x => x.ValidUntil).WithMessage("The start date cannot be greater than the end date.").Must(x => x.Year > 2010).WithMessage("The start date cannot be earlier than 2010.").NotEmpty().WithMessage("This field is required.");
            RuleFor(x => x.ValidUntil).NotEmpty().WithMessage("This field is required.");

        }
    }
}
