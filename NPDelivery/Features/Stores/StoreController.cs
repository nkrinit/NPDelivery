using Mediator;

using Microsoft.AspNetCore.Mvc;

using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;
[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IMediator _mediator;
    public StoreController(IMediator mediator)
    {
        _mediator= mediator;
    }
 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetStoreResult>> Get(int id)
    {
        var query = new GetStoreQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateStoreCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<GetStoreResult>> Put(UpdateStoreCommand command)
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
