﻿using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        CreateMap<Guid, Application.Sales.DeleteSale.DeleteSaleHandler>()
            .ConstructUsing(id => new Application.Sales.DeleteSale.DeleteSaleCommand(id));
    }
}