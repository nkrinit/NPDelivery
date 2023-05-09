using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;

namespace NPDelivery.Features.Orders;

public sealed record CreateOrderCommand(int CustomerId, int StoreId, List<OrderedProduct> Products, string From, string To) 
    : ICommand<Order>;

public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly DataContext _context;

    public CreateOrderHandler(DataContext dataContext)
    {
        _context = dataContext;
    }
    public ValueTask<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _context.Orders.Add(new Order(request.CustomerId, request.StoreId, request.Products, request.From, request.To));

        return ValueTask.FromResult(order.Entity);
    }
}
