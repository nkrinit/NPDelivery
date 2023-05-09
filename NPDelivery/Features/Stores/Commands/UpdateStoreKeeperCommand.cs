using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;
using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;

public sealed record UpdateStoreKeeperCommand(int StoreKeeperId) : ICommand<Result<GetStoreKeeperResult>>;

public sealed class UpdateStoreKeeperHandler : ICommandHandler<UpdateStoreKeeperCommand, Result<GetStoreKeeperResult>>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public UpdateStoreKeeperHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetStoreKeeperResult>> Handle(UpdateStoreKeeperCommand request, CancellationToken cancellationToken)
    {
        var storeKeeper = await _context.StoreKeepers
            .AsTracking()
            .FirstOrDefaultAsync(s => s.Id == request.StoreKeeperId, cancellationToken: cancellationToken);

        if (storeKeeper == null)
        {
            return new NotFoundError();
        }

        // Update

        var result = _mapper.StoreKeeperToGetStoreKeeperResult(storeKeeper);

        return result;
    }
}
