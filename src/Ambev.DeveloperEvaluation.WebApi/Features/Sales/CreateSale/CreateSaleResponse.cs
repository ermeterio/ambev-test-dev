﻿using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Domain.Entities.Company.Company? Company { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string? Code { get; set; }
    public IReadOnlyCollection<SaleDiscount>? Discounts { get; set; }
    public IReadOnlyCollection<SaleItem>? Products { get; set; }
    public SaleStatus Status { get; set; }
}