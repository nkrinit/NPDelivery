using NPDelivery.Domain;

using Riok.Mapperly.Abstractions;

namespace NPDelivery.Features.Couriers;

[Mapper]
public partial class CourierMapper
{
    [MapProperty(nameof(Courier.Id), nameof(GetCourierResult.CourierId))]
    public partial GetCourierResult CourierToGetCourierResult(Courier courier);
}