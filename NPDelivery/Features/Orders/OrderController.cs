using Mediator;

using Microsoft.AspNetCore.Mvc;

using Remora.Results;

namespace NPDelivery.Features.Orders;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetStatusAsync(int id)
    {
        var query = new GetOrderQuery(id);
        var result = await _mediator.Send(query);

        // to do: think deeply about moving this to a separate error handler. Otherwise you risk to add a new error type each it's introduced
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

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateOrderCommand command) 
    {
        var result = await _mediator.Send(command);
        //to do: return a result
        return Ok();
    }


}
