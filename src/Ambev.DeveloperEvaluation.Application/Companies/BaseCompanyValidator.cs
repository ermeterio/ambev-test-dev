using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Application.Companies
{
    public class BaseCompanyValidator<T> : AbstractValidator<T> where T : BaseCompany
    {
        public BaseCompanyValidator()
        {
            RuleFor(c => c.Cnpj).NotEmpty().Length(14);
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
            RuleFor(company => IsValidCnpj(company.Cnpj)).NotEqual(false).WithMessage("Invalid Cnpj");
        }

        private static bool IsValidCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");
            if (cnpj.Length != 14 || new string(cnpj[0], 14) == cnpj) return false;

            var cnpjNumbers = cnpj.Select(c => c - '0').ToArray();
            return cnpjNumbers[12] == DigitCalculate(cnpjNumbers.Take(12).ToArray()) &&
                   cnpjNumbers[13] == DigitCalculate(cnpjNumbers.Take(13).ToArray());
        }

        private static int DigitCalculate(IReadOnlyCollection<int> cnpj)
        {
            int[] multiplicadores = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 6, 5];
            var sum = cnpj.Select((t, i) => t * multiplicadores[multiplicadores.Length - cnpj.Count + i]).Sum();

            var rest = sum % 11;
            return (rest < 2) ? 0 : 11 - rest;
        }
    }
}