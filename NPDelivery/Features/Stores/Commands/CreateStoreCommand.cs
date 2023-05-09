using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;
using NPDelivery.Features.Stores.Dtos;

namespace NPDelivery.Features.Stores;

public sealed record CreateStoreCommand(string Name, string Description, string Address, int StoreKeeperId) : ICommand<GetStoreResult>;

public sealed class CreateStoreHandler : ICommandHandler<CreateStoreCommand, GetStoreResult>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public CreateStoreHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<GetStoreResult> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = _context.Stores.Add(new Store(request.Name, request.Description, request.StoreKeeperId, request.Address));

        var result = _mapper.StoreToGetStoreResult(store.Entity);

        return new ValueTask<GetStoreResult>(result);
    }
}
