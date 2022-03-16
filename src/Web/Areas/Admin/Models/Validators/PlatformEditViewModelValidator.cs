using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class PlatformEditViewModelValidator : AbstractValidator<PlatformEditViewModel>
    {
        public PlatformEditViewModelValidator()
        {
            RuleFor(x => x.PlatformName)
                    .NotNull().WithMessage("This field is required.")
                    .NotEmpty().WithMessage("This field is required.")
                    .MaximumLength(100).WithMessage("This field has max length of {0}");

            RuleFor(x => x.LogoPath)
                    .MaximumLength(100).WithMessage("This field has max length of {0}");

            RuleFor(x => x.LogoImage)
                    .SetValidator(new FileValidator(2));
        }
    }
}
