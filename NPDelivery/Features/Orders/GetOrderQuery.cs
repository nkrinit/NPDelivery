using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Orders;

public sealed record GetOrderQuery(int OrderId) : IQuery<Result<GetOrderResult>>;

public sealed record GetOrderResult(int OrderId, int CustomerId, int StoreId, int? CourierId, string From, string To);

public sealed class GetOrderHandler : IQueryHandler<GetOrderQuery, Result<GetOrderResult>>
{
    private readonly DataContext _context;
    private readonly OrderMapper _mapper;

    public GetOrderHandler(DataContext dataContext, OrderMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }
    public async ValueTask<Result<GetOrderResult>> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == query.OrderId, cancellationToken: cancellationToken);

        if(order == null)
        {
            return new NotFoundError();
        }

        var result = _mapper.OrderToGetOrderResult(order);

        return result;
    }
}
