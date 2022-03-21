using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class GameEditViewModelValidator : AbstractValidator<GameEditViewModel>
    {
        static readonly string notNullMessage = "This field is required.";
        static readonly string maxLengthMessage = "This field has max length of {0}";
        public GameEditViewModelValidator()
        {
            RuleFor(x => x.GameName)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(200).WithMessage(maxLengthMessage);
            RuleFor(x => x.Description)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage);
            RuleFor(x => x.GameRequirements)
               .NotNull().WithMessage(notNullMessage)
               .NotEmpty().WithMessage(notNullMessage);
            RuleFor(x => x.Developer)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(200).WithMessage(maxLengthMessage);
            RuleFor(x => x.Publisher)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(200).WithMessage(maxLengthMessage);
            RuleFor(x => x.TrailerUrl)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(2048).WithMessage(maxLengthMessage);
            RuleFor(x => x.ImagePath)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .MaximumLength(200).WithMessage(maxLengthMessage);
            RuleFor(x => x.GenreIds)
                .NotNull().WithMessage(notNullMessage)
                .Must(x => x.Count <= 5).WithMessage("You can choose max 5 genres.");
            RuleFor(x => x.GameImage)
                .SetValidator(new FileValidator(2));
        }
    }
}
