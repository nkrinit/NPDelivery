using NPDelivery.Domain;

using Riok.Mapperly.Abstractions;

namespace NPDelivery.Features.Orders;

[Mapper]
public partial class OrderMapper
{
    [MapProperty(nameof(Order.Id), nameof(GetOrderResult.OrderId))]
    public partial GetOrderResult OrderToGetOrderResult(Order order);
}
