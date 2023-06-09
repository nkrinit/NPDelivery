﻿using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NPDelivery.Auth;

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
    public async Task<ActionResult<GetOrderResult>> Get(int id)
    {
        var query = new GetOrderQuery(id);
        var result = await _mediator.Send(query);

        return VerifyResult(result);
    }

    [Authorize(Policy = PolicyNames.Customer)]
    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderCommand command) 
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    //To do: add PUT endpoint

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
