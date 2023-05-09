using NPDelivery.Features.Products;

namespace NPDelivery.Domain;

public static class PriceExtentions
{
    public static void ConvertPriceFrom(this IHasConvertedPrice hasConvertedPrice, IHasPrice hasPrice)
    {
        hasConvertedPrice.Price = (decimal)hasPrice.Price / 100;
    }
}
