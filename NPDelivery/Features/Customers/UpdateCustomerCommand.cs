using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Customers;

public sealed record UpdateCustomerCommand(int CustomerId, string Name, string Surname, string Address, string Email, string Phone) : ICommand<Result<GetCustomerResult>>;

public sealed class UpdateCustomerHandler : ICommandHandler<UpdateCustomerCommand, Result<GetCustomerResult>>
{
    private readonly DataContext _context;
    private readonly CustomerMapper _mapper;

    public UpdateCustomerHandler(DataContext dataContext, CustomerMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetCustomerResult>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == request.CustomerId, cancellationToken: cancellationToken);

        if (customer == null)
        {
            return new NotFoundError();
        }

        customer.Update(request.Name, request.Surname, request.Address, request.Email, request.Phone);

        var result = _mapper.CustomerToGetCustomerResult(customer);

        return result;
    }
}
