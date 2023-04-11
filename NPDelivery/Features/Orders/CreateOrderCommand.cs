using Mediator;

namespace NPDelivery.Features.Orders;

public sealed record CreateOrderCommand(int CustomerId, int StoreId, int MenuItemId, int Quantity) : ICommand;

public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly Database _database;

    public CreateOrderHandler(Database database)
    {
        _database = database;
    }
    public ValueTask<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _database.AddOrder(new Order(request.CustomerId, request.StoreId, request.MenuItemId, request.Quantity));
        //to do: return the new order
        return new ValueTask<Unit>();
    }
}
