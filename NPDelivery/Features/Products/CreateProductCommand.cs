using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;

namespace NPDelivery.Features.Products;

public sealed record CreateProductCommand(string Name, string Description, int Price, int StoreId) : ICommand<GetProductResult>;

public sealed class CreateProductHandler : ICommandHandler<CreateProductCommand, GetProductResult>
{
    private readonly DataContext _context;
    private readonly ProductMapper _mapper;

    public CreateProductHandler(DataContext dataContext, ProductMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<GetProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _context.Products.Add(new Product(request.Name, request.Description, request.Price, request.StoreId));

        var result = _mapper.MapProductToGetProductResult(product.Entity);

        return ValueTask.FromResult(result);
    }
}
