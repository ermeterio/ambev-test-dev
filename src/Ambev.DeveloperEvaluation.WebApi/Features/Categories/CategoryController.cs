using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.Application.Categories.GetCategory;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories
{
    /// <summary>
    /// Controller for managing user operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="request">The Category creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created category details</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCategoryResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateCategoryCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateCategoryResponse>
            {
                Success = true,
                Message = "Category created successfully",
                Data = _mapper.Map<CreateCategoryResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves a category by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the category</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The category details if found</returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCategoryRequest { Id = id };
            var validator = new GetCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetCategoryCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response.Id == default)
                return NotFound(new ApiResponseWithData<GetCategoryResponse>
                {
                    Success = false,
                    Message = "Not found",
                });

            return Ok(new ApiResponseWithData<GetCategoryResponse>
            {
                Success = true,
                Message = "Category retrieved successfully",
                Data = _mapper.Map<GetCategoryResponse>(response)
            });
        }

        /// <summary>
        /// Deletes a Category by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the Category was deleted</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteCategoryRequest { Id = id };
            var validator = new DeleteCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteCategoryCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Category deleted successfully"
            });
        }
    }
}
