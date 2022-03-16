using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class GenreViewModelValidator : AbstractValidator<GenreVieModel>
    {
        public GenreViewModelValidator()
        {
            RuleFor(x => x.GenreName)
                .NotNull().WithMessage("This field is required.")
                .NotEmpty().WithMessage("This field is required.")
                .MaximumLength(200).WithMessage($"This field has max length of {0}");
        }
    }
}
