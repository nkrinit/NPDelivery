using Mediator;

using Microsoft.AspNetCore.Mvc;

using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;
[Route("api/[controller]")]
[ApiController]
public class StoreKeeperController : ControllerBase
{
    private readonly IMediator _mediator;
    public StoreKeeperController(IMediator mediator)
    {
        _mediator= mediator;
    }
 
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetStoreKeeperResult>> Get(int id)
    {
        var query = new GetStoreKeeperQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateStoreKeeperCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<GetStoreKeeperResult>> Put(UpdateStoreKeeperCommand command)
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
