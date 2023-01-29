using FluentValidation;
using Spg.SpengerShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Validators
{
    public class NewCustomerDtoValidator : AbstractValidator<NewCustomerDto>
    {
        public NewCustomerDtoValidator()
        {
            RuleFor(p => p.LastName)
                .Length(5, 20)
                .WithMessage("Länge BITTE zwischen 5 und 20 (G'sicht)!");
        }
    }
}
