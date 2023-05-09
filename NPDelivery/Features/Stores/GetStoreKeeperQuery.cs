using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;
using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;

public sealed record GetStoreKeeperQuery(int StoreKeeperId) : IQuery<Result<GetStoreKeeperResult>>;

public sealed class GetStoreKeeperHandler : IQueryHandler<GetStoreKeeperQuery, Result<GetStoreKeeperResult>>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public GetStoreKeeperHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context= dataContext;
        _mapper= mapper;
    }

    public async ValueTask<Result<GetStoreKeeperResult>> Handle(GetStoreKeeperQuery query, CancellationToken cancellationToken)
    {
        var storeKeeper = await _context.StoreKeepers
            .AsNoTracking()
            .Include(s => s.Stores)
            .FirstOrDefaultAsync(s => s.Id == query.StoreKeeperId, cancellationToken: cancellationToken);
        
        if (storeKeeper == null)
        {
            return new NotFoundError();
        }

        return _mapper.StoreKeeperToGetStoreKeeperResult(storeKeeper);
    }
}