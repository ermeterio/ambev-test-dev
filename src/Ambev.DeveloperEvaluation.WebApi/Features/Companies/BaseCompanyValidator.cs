using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies;

public class BaseCompanyValidator<T> : AbstractValidator<T> where T : BaseCompany
{
    public BaseCompanyValidator()
    {
        RuleFor(company => IsCnpj(company.Cnpj)).NotEqual(false).WithMessage("Invalid Cnpj");
        RuleFor(company => company.Name).NotEmpty().MaximumLength(100);
    }

    public static bool IsCnpj(string cnpj)
    {
        var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int sum;
        int remainder;
        string digit;
        string tempCnpj;

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        tempCnpj = cnpj[..12];

        sum = 0;
        for (var i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        remainder = (sum % 11);
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit = remainder.ToString();

        tempCnpj += digit;
        sum = 0;
        for (var i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        remainder = (sum % 11);
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit += remainder.ToString();

        return cnpj.EndsWith(digit, StringComparison.Ordinal);
    }
}