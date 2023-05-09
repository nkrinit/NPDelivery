using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Products;

public sealed record GetProductQuery(int ProductId) : IQuery<Result<GetProductResult>>;

public sealed class GetProductResult : IHasConvertedPrice
{
    // to do: why productId doesn't work
    public GetProductResult(int id, string name, string description, decimal price, int storeId)
    {
        ProductId = id;
        Name = name;
        Description = description;
        Price = price;
        StoreId = storeId;
    }

    public int ProductId { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; set; }
    public int StoreId { get; }
}

public sealed class GetProductHandler : IQueryHandler<GetProductQuery, Result<GetProductResult>>
{
    private readonly DataContext _context;
    private readonly ProductMapper _mapper;

    public GetProductHandler(DataContext dataContext, ProductMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetProductResult>> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == query.ProductId, cancellationToken: cancellationToken);

        if(product == null)
        {
            return new NotFoundError();
        }

        var result = _mapper.MapProductToGetProductResult(product);

        return result;
    }
}
