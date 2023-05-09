using NPDelivery.Domain;

using Riok.Mapperly.Abstractions;

namespace NPDelivery.Features.Products;

[Mapper]
public partial class ProductMapper
{
    public GetProductResult MapProductToGetProductResult(Product product)
    {
        var target = ProductToGetProductResult(product);
        target.ConvertPriceFrom(product);
        return target;
    }

    [MapProperty(nameof(Product.Id), nameof(GetProductResult.ProductId))]
    private partial GetProductResult ProductToGetProductResult(Product product);
}
