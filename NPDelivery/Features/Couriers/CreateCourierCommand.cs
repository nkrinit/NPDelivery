using Mediator;

using NPDelivery.Data;
using NPDelivery.Domain;

namespace NPDelivery.Features.Couriers;

public sealed record CreateCourierCommand(string Name, string Surname, string Email, string Phone) : ICommand<GetCourierResult>;

public sealed class CreateCourierHandler : ICommandHandler<CreateCourierCommand, GetCourierResult>
{
    private readonly DataContext _context;
    private readonly CourierMapper _mapper;

    public CreateCourierHandler(DataContext dataContext, CourierMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public ValueTask<GetCourierResult> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = _context.Couriers.Add(new Courier(request.Name, request.Surname, request.Email, request.Phone));

        var result = _mapper.CourierToGetCourierResult(courier.Entity);

        return ValueTask.FromResult(result);
    }
}
