using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;

namespace NPDelivery.Features.Orders;

public sealed record CreateOrderCommand(int CustomerId, int StoreId, List<OrderedProductDto> Products, string From, string To) 
    : ICommand<GetOrderResult>;
public sealed record OrderedProductDto(int ProductId, int Quantity);


public sealed class CreateOrderHandler : ICommandHandler<CreateOrderCommand, GetOrderResult>
{
    private readonly DataContext _context;
    private readonly OrderMapper _mapper;

    public CreateOrderHandler(DataContext dataContext, OrderMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<GetOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId, request.StoreId, request.From, request.To);

        foreach (var product in request.Products)
        {
            order.AddProduct(product.ProductId, product.Quantity);
        }

        await _context.Orders.AddAsync(order);

        var result = _mapper.OrderToGetOrderResult(order);

        return result;
    }
}
