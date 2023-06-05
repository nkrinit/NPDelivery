using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Couriers;

public sealed record UpdateCourierCommand(int CourierId, string Name, string Surname, string Email, string Phone) : ICommand<Result<GetCourierResult>>;

public sealed class UpdateCustomerHandler : ICommandHandler<UpdateCourierCommand, Result<GetCourierResult>>
{
    private readonly DataContext _context;
    private readonly CourierMapper _mapper;

    public UpdateCustomerHandler(DataContext dataContext, CourierMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }

    public async ValueTask<Result<GetCourierResult>> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await _context.Couriers
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == request.CourierId, cancellationToken: cancellationToken);

        if (courier == null)
        {
            return new NotFoundError();
        }

        courier.Update(request.Name, request.Surname, request.Email, request.Phone);

        var result = _mapper.CourierToGetCourierResult(courier);

        return result;
    }
}
