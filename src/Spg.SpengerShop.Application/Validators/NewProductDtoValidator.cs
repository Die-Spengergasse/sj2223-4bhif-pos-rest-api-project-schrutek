using FluentValidation;
using Spg.SpengerShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Validators
{
    public class NewProductDtoValidator : AbstractValidator<NewProductDto>
    {
        public NewProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(20)
                .MinimumLength(3)
                .WithMessage("Bitte zwischen 3 und 20!!!")
                .WithErrorCode("9000");
        }
    }
}
