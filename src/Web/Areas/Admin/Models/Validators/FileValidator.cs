using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Models.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        private readonly int _maxMb;

        public FileValidator(int maxMb = 1)
        {
            _maxMb = maxMb;

            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(_maxMb * 1024 * 1024)
                .WithMessage("File size is larger than allowed");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.StartsWith("image/"))
                .WithMessage("File type is not supported");
        }
    }
}
