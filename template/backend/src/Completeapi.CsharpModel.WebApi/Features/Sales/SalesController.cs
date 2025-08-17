using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Completeapi.CsharpModel.WebApi.Common;
using Completeapi.CsharpModel.WebApi.Features.Sales.CreateSale;
// using Completeapi.CsharpModel.WebApi.Features.Sales.GetSale;
// using Completeapi.CsharpModel.WebApi.Features.Sales.DeleteSale;
using Completeapi.CsharpModel.Application.Sales.CreateSale;
using Completeapi.CsharpModel.Application.Sales.DeleteSale;
using Completeapi.CsharpModel.WebApi.Features.Sales.DeleteSale;
using Microsoft.AspNetCore.Authorization;
using Completeapi.CsharpModel.WebApi.Features.Sales.UpdateSale;
using Completeapi.CsharpModel.Application.Sales.UpdateSale;
using Completeapi.CsharpModel.Application.Sales.ListSale;
using Completeapi.CsharpModel.WebApi.Features.Sales.ListSale;
using Completeapi.CsharpModel.Application.Sales.GetSaleById;
using Completeapi.CsharpModel.WebApi.Features.Sales.GetSaleById;

// using Completeapi.CsharpModel.Application.Sales.GetSale;
// using Completeapi.CsharpModel.Application.Sales.DeleteSale;

namespace Completeapi.CsharpModel.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SalesController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(
        typeof(ApiResponseWithData<CreateSaleResponse>),
        StatusCodes.Status201Created
    )]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale(
        [FromBody] CreateSaleRequest request,
        CancellationToken cancellationToken
    )
    {
        // Extrai o token dos headers da requisição
        string token = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        request.Token = token;


        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(
            string.Empty,
            new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(response)
            }
        );
    }
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<PaginatedList<ListSaleResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListSales([FromQuery] ListSaleRequest request, CancellationToken cancellationToken)
    {
        string token = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        
        var validator = new ListSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<ListSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        var response = new PaginatedList<ListSaleResponse>(
            _mapper.Map<List<ListSaleResponse>>(result),
            result.TotalCount,
            result.CurrentPage,
            result.PageSize
        );

        return Ok(new ApiResponseWithData<PaginatedList<ListSaleResponse>>
        {
            Success = true,
            Message = "Sales retrieved successfully",
            Data = response
        });
    }



    // /// <summary>
    // /// Retrieves a sale by their ID
    // /// </summary>
    // /// <param name="id">The unique identifier of the sale</param>
    // /// <param name="cancellationToken">Cancellation token</param>
    // /// <returns>The sale details if found</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        string token = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        var request = new GetSaleByIdRequest { Id = id, Token = token };
        var command = _mapper.Map<GetSaleByIdCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponseWithData<GetSaleByIdResponse>
        {
            Success = true,
            Message = "Sale retrieved successfully",
            Data = _mapper.Map<GetSaleByIdResponse>(result)
        });
    }


    /// <summary>
    /// Deletes a sale by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the sale was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        // Extrai token do header
        string token = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        var request = new DeleteSaleRequest { Id = id, Token = token };
        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse { Success = true, Message = "Sale deleted successfully" });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSale(
    [FromRoute] Guid id,
    [FromBody] UpdateSaleRequest request,
    CancellationToken cancellationToken
)
    {
        // Extrai token do header
        string token = HttpContext.Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        request.Token = token;
        request.Id = id;

        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<UpdateSaleResponse>
        {
            Success = true,
            Message = "Sale updated successfully",
            Data = _mapper.Map<UpdateSaleResponse>(response)
        });
    }

}
