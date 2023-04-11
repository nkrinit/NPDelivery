using Mediator;

using Remora.Results;

namespace NPDelivery.Features.Orders;

public sealed record GetOrderQuery(int OrderId) : IQuery<Result<Order>>;

public sealed class GetOrderHandler : IQueryHandler<GetOrderQuery, Result<Order>>
{
    private readonly Database _database;

    public GetOrderHandler(Database database)
    {
        _database = database;
    }
    public ValueTask<Result<Order>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = _database.Orders.FirstOrDefault(o => o.Id == query.OrderId);
        if(order == null)
        {
            return new ValueTask<Result<Order>>(new NotFoundError());
        }
        // to do: convert this to DTO, never return a domain! Consider using younger brothers of AutoMapper
        return new ValueTask<Result<Order>>(order);
    }
}
