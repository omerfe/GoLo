using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class KeyViewModelValidator:AbstractValidator<KeyViewModel>
    {
        public KeyViewModelValidator()
        {
            RuleFor(x => x.KeyCode).NotEmpty().WithMessage("This field is required and must be in key format.");
        }
    }
}
