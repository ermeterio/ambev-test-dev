﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleValidator()
        {
            RuleFor(s => s.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}