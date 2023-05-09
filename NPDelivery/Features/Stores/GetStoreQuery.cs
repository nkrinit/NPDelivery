using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;
using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;

public sealed record GetStoreQuery(int StoreId) : IQuery<Result<GetStoreResult>>;

public sealed class GetStoreHandler : IQueryHandler<GetStoreQuery, Result<GetStoreResult>>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public GetStoreHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context= dataContext;
        _mapper= mapper;
    }

    public async ValueTask<Result<GetStoreResult>> Handle(GetStoreQuery query, CancellationToken cancellationToken)
    {
        var store = await _context.Stores
            .AsNoTracking()
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == query.StoreId, cancellationToken: cancellationToken);
        
        if (store == null)
        {
            return new NotFoundError();
        }

        return _mapper.StoreToGetStoreResult(store);
    }
}