using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class GameViewModelValidator : AbstractValidator<GameViewModel>
    {
        static readonly string notNullMessage = "This field is required.";
        static readonly string maxLengthMessage = "This field has max length of {0}";
        public GameViewModelValidator()
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
                .MaximumLength(200).WithMessage(maxLengthMessage);
            RuleFor(x => x.GenreIds)
                .NotNull().WithMessage(notNullMessage)
                .Must(x=>x.Count <= 5).WithMessage("You can choose max 5 genres.");
            RuleFor(x => x.GameImage)
                .NotNull().WithMessage(notNullMessage)
                .NotEmpty().WithMessage(notNullMessage)
                .SetValidator(new FileValidator(2));

        }
    }
}
