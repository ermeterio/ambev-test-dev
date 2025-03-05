using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;
using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies.GetCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies.UpdateCompany;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies
{
    /// <summary>
    /// Controller for managing user operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CompanyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new company
        /// </summary>
        /// <param name="request">The Company creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created company details</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCompanyResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateCompanyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCompanyCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateCompanyResponse>
            {
                Success = true,
                Message = "Company created successfully",
                Data = _mapper.Map<CreateCompanyResponse>(response)
            });
        }

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="request">The Company update request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created Company details</returns>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateCompanyResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCompanyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateCompanyCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<UpdateCompanyResponse>
            {
                Success = true,
                Message = "Company updated successfully",
                Data = _mapper.Map<UpdateCompanyResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves a company by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the company</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The company details if found</returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCompanyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompany([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCompanyRequest { Id = id };
            var validator = new GetCompanyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetCompanyCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response.Id == default)
                return NotFound(new ApiResponseWithData<GetCompanyResult>
                {
                    Success = false,
                    Message = "Not found",
                });

            return Ok(new ApiResponseWithData<GetCompanyResponse>
            {
                Success = true,
                Message = "Company retrieved successfully",
                Data = _mapper.Map<GetCompanyResponse>(response)
            });
        }

        /// <summary>
        /// Deletes a Company by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the company to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the Company was deleted</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompany([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteCompanyRequest { Id = id };
            var validator = new DeleteCompanyRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCompanyCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Company deleted successfully"
            });
        }
    }
}