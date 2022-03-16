using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    //TODO Validator'ler tek bir generic validator yazılıp aynı rule'a sahip olanlar bunu çağıracak şekilde yapılacak(denenecek)
    public class GenreViewModelValidator : AbstractValidator<GenreViewModel>
    {
        public GenreViewModelValidator()
        {
            RuleFor(x => x.GenreName)
                .NotNull().WithMessage("This field is required.")
                .NotEmpty().WithMessage("This field is required.")
                .MaximumLength(200).WithMessage("This field has max length of {0}");
        }
    }
}
