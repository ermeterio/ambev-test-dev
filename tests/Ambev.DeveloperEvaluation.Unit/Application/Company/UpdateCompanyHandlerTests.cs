using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.Fixture;
using AutoMapper;
using Moq.AutoMock;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company
{
    [Collection(nameof(CompanyTestsFixtureCollection))]
    public class UpdateCompanyHandlerTests
    {
        private readonly CompanyTestsFixture _fixture;
        private readonly AutoMocker _mocker;
        public UpdateCompanyHandlerTests(CompanyTestsFixture fixture)
        {
            _fixture = fixture;
            _mocker = new AutoMocker();
        }
        #region Success
        [Fact(DisplayName = nameof(Should_Be_Update_Successful_Company))]
        [Trait("Company", nameof(UpdateCompanyHandlerTests))]
        public async Task Should_Be_Update_Successful_Company()
        {
            //arrange
            var companyRepository = Substitute.For<ICompanyRepository>();
            var mapper = Substitute.For<IMapper>();
            mapper.Map<DeveloperEvaluation.Domain.Entities.Company.Company>(Arg.Any<UpdateCompanyCommand>())
                .Returns(_fixture.GetValidCompany());
            companyRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(_fixture.GetValidCompany());
            companyRepository.UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company.Company>(), Arg.Any<CancellationToken>()).Returns(_fixture.GetValidCompany());
            var commandHandler = new UpdateCompanyHandler(companyRepository, mapper);

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.Null(exceptions);
        }
        #endregion
        #region Error
        [Fact(DisplayName = nameof(Should_Be_Unsuccessful_Update_Not_Found_Company))]
        [Trait("Company", nameof(UpdateCompanyHandlerTests))]
        public async Task Should_Be_Unsuccessful_Update_Not_Found_Company()
        {
            //arrange
            var repository = Substitute.For<ICompanyRepository>();
            repository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
            var commandHandler = new UpdateCompanyHandler(repository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }

        [Fact(DisplayName = nameof(Should_Be_Update_Invalid_Company))]
        [Trait("Company", nameof(CreateCompanyHandlerTests))]
        public async Task Should_Be_Update_Invalid_Company()
        {
            //arrange
            var company = _fixture.GetInvalidCompany();
            var companyRepository = Substitute.For<ICompanyRepository>();
            companyRepository.GetByCnpjAsync(company.Cnpj, CancellationToken.None).Returns(company);
            var commandHandler = new UpdateCompanyHandler(companyRepository, Substitute.For<IMapper>());

            //act
            var exceptions = await Record.ExceptionAsync(() => commandHandler.Handle(_fixture.ValidUpdateCompanyCommand(), CancellationToken.None));

            //assert
            Assert.NotNull(exceptions);
        }
        #endregion
    }
}