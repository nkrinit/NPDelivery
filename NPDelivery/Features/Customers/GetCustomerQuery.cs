using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Customers;

public sealed record GetCustomerQuery(int CustomerId) : IQuery<Result<GetCustomerResult>>;

public sealed record GetCustomerResult(int CustomerId, string Name, string Surname, string Address);

public sealed class GetCustomerHandler : IQueryHandler<GetCustomerQuery, Result<GetCustomerResult>>
{
    private readonly DataContext _context;
    private readonly CustomerMapper _mapper;

    public GetCustomerHandler(DataContext dataContext, CustomerMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }
    public async ValueTask<Result<GetCustomerResult>> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == query.CustomerId, cancellationToken: cancellationToken);

        if(customer == null)
        {
            return new NotFoundError();
        }

        var result = _mapper.CustomeroGetCustomerResult(customer);

        return result;
    }
}
