using NPDelivery.Domain;

using Riok.Mapperly.Abstractions;

namespace NPDelivery.Features.Customers;

[Mapper]
public partial class CustomerMapper
{
    [MapProperty(nameof(Customer.Id), nameof(GetCustomerResult.CustomerId))]
    public partial GetCustomerResult CustomerToGetCustomerResult(Customer customer);
}