using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;
using NPDelivery.Features.Stores.Dtos;

namespace NPDelivery.Features.Stores;

public sealed record CreateStoreKeeperCommand() : ICommand<GetStoreKeeperResult>;

public sealed class CreateStoreKeeperHandler : ICommandHandler<CreateStoreKeeperCommand, GetStoreKeeperResult>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public CreateStoreKeeperHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<GetStoreKeeperResult> Handle(CreateStoreKeeperCommand request, CancellationToken cancellationToken)
    {
        var store = _context.StoreKeepers.Add(new StoreKeeper());

        var result = _mapper.StoreKeeperToGetStoreKeeperResult(store.Entity);

        return new ValueTask<GetStoreKeeperResult>(result);
    }
}
