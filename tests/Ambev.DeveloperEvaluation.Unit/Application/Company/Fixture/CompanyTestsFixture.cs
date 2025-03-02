﻿using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture
{
    [CollectionDefinition(nameof(CompanyTestsFixtureCollection))]
    public class CompanyTestsFixtureCollection : ICollectionFixture<CompanyTestsFixture>;

    public class CompanyTestsFixture
    {
        private readonly Faker _faker;
        public CompanyTestsFixture()
        {
            _faker = new Faker("pt-BR");
        }

        public CreateCompanyCommand ValidCreateCompanyCommand()
        {
            return new CreateCompanyCommand
            {
                Name = _faker.Name.FullName(),
                Cnpj = GenerateValidCnpj()
            };
        }

        public UpdateCompanyCommand ValidUpdateCompanyCommand()
        {
            return new UpdateCompanyCommand
            {
                Id = new Guid(),
                Name = _faker.Name.FullName(),
                Cnpj = GenerateValidCnpj()
            };
        }

        static string GenerateValidCnpj()
        {
            var rnd = new Random();
            var cnpjBase = new int[12];

            for (var i = 0; i < 12; i++)
                cnpjBase[i] = rnd.Next(0, 9);

            var completeCnpj = new int[14];
            Array.Copy(cnpjBase, completeCnpj, 12);

            completeCnpj[12] = DigitCalculator(cnpjBase);
            completeCnpj[13] = DigitCalculator(completeCnpj.Take(13).ToArray());

            return string.Join("", completeCnpj);
        }

        static int DigitCalculator(IReadOnlyCollection<int> cnpj)
        {
            int[] multiplier = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2, 6, 5];
            var soma = cnpj.Select((t, i) => t * multiplier[multiplier.Length - cnpj.Count + i]).Sum();

            var rest = soma % 11;
            return (rest < 2) ? 0 : 11 - rest;
        }
    }
}