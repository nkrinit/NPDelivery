using Mediator;

using Microsoft.AspNetCore.Mvc;

using Remora.Results;

namespace NPDelivery.Features.Couriers;
[Route("api/[controller]")]
[ApiController]
public class CourierController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetCourierResult>> Get(int id)
    {
        var query = new GetCourierQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCourierCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }


    [HttpPut]
    public async Task<ActionResult<GetCourierResult>> Put(UpdateCourierCommand command)
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
