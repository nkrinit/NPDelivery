﻿using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;

namespace NPDelivery.Features.Customers;

public sealed record CreateCustomerCommand(string Name, string Surname, string Address, string Email, string Phone) : ICommand<GetCustomerResult>;

public sealed class CreateCustomerHandler : ICommandHandler<CreateCustomerCommand, GetCustomerResult>
{
    private readonly DataContext _context;
    private readonly CustomerMapper _mapper;

    public CreateCustomerHandler(DataContext dataContext, CustomerMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<GetCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _context.Customers.Add(new Customer(request.Name, request.Surname, request.Address, request.Email, request.Phone));

        var result = _mapper.CustomerToGetCustomerResult(customer.Entity);

        return ValueTask.FromResult(result);
    }
}
