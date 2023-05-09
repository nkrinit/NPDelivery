using Mediator;

using Microsoft.AspNetCore.Mvc;

using Remora.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NPDelivery.Features.Products;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetProductResult>> Get(int id)
    {
        var query = new GetProductQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<GetProductResult>> Put(UpdateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return VerifyResult(result);
    }

    private ActionResult<T> VerifyResult<T>(Result<T> result)
    {
        if (!result.IsSuccess)
        {
            return result.Error switch
            {
                NotFoundError notFoundError => NotFound(notFoundError.Message),
                _ => Problem(detail: result.Error.Message, title: "Internal Server Error", statusCode: 500)
            };
        }

        return result.Entity;
    }

}
