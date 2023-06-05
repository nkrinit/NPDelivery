using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;
using NPDelivery.Features.Stores.Dtos;

namespace NPDelivery.Features.Stores;

public sealed record CreateStoreCommand(string Name, string Description, string Address, int StoreKeeperId) : ICommand<CreateStoreResult>;

public sealed class CreateStoreHandler : ICommandHandler<CreateStoreCommand, CreateStoreResult>
{
    private readonly DataContext _context;
    private readonly StoreMapper _mapper;

    public CreateStoreHandler(DataContext dataContext, StoreMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<CreateStoreResult> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = _context.Stores.Add(new Store(request.Name, request.Description, request.StoreKeeperId, request.Address));

        var result = _mapper.StoreToCreateStoreResult(store.Entity);

        return new ValueTask<CreateStoreResult>(result);
    }
}
