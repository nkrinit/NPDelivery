using Mediator;

using Microsoft.AspNetCore.Mvc;

using Remora.Results;

namespace NPDelivery.Features.Customers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetCustomerResult>> GetAsync(int id)
    {
        var query = new GetCustomerQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCustomerCommand command) 
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<GetCustomerResult>> Put(UpdateCustomerCommand command)
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
