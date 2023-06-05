using Mediator;

using Microsoft.EntityFrameworkCore;

using NPDelivery.Data;

using Remora.Results;

namespace NPDelivery.Features.Couriers;

public sealed record GetCourierQuery(int CourierId) : IQuery<Result<GetCourierResult>>;

public sealed record GetCourierResult(int CourierId, string Name, string Surname, string Phone);

public sealed class GetCourierHandler : IQueryHandler<GetCourierQuery, Result<GetCourierResult>>
{
    private readonly DataContext _context;
    private readonly CourierMapper _mapper;

    public GetCourierHandler(DataContext dataContext, CourierMapper mapper)
    {
        _context = dataContext;
        _mapper = mapper;
    }
    public async ValueTask<Result<GetCourierResult>> Handle(GetCourierQuery query, CancellationToken cancellationToken)
    {
        var courier = await _context.Couriers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == query.CourierId, cancellationToken: cancellationToken);

        if(courier == null)
        {
            return new NotFoundError();
        }

        var result = _mapper.CourierToGetCourierResult(courier);

        return result;
    }
}
