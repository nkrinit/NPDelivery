using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Products;

public sealed record UpdateProductCommand(int ProductId, string Name, string Description, int Price, bool IsAvailable) : ICommand<Result<GetProductResult>>;

public sealed class UpdateProductHandler : ICommandHandler<UpdateProductCommand, Result<GetProductResult>>
{
    private readonly DataContext _context;
    private readonly ProductMapper _mapper;

    public UpdateProductHandler(DataContext dataContext, ProductMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);

        if (product == null)
        {
            return new NotFoundError();
        }

        product.Update(request.Name, request.Description, request.Price, request.IsAvailable);

        var result = _mapper.MapProductToGetProductResult(product);

        return result;
    }
}
