using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;
using NPDelivery.Domain;
using NPDelivery.Features.Stores.Dtos;

using Remora.Results;

namespace NPDelivery.Features.Stores;

public sealed record UpdateStoreCommand(int StoreId, string Name, string Description, string Address) : ICommand<Result<GetStoreResult>>;

public sealed class UpdateStoreHandler : ICommandHandler<UpdateStoreCommand, Result<GetStoreResult>>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public UpdateStoreHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetStoreResult>> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = await _context.Stores
            .AsTracking()
            .FirstOrDefaultAsync(s => s.Id == request.StoreId, cancellationToken: cancellationToken);

        if (store == null)
        {
            return new NotFoundError();
        }

        store.Update(request.Name, request.Description, request.Address);

        var result = _mapper.StoreToGetStoreResult(store);

        return result;
    }
}
