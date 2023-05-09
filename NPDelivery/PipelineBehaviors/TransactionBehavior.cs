using Mediator;

using NPDelivery.Data;

namespace NPDelivery.PipelineBehaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
{
    private readonly DataContext _context;
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(DataContext context, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        Guid transactionId = default;
        var commandName = typeof(TRequest).Name;
        try
        {
            if(_context.Database.CurrentTransaction is not null)
            {
                return await next(message, cancellationToken);
            }

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            transactionId = transaction.TransactionId;
            // to do: push logging property
            _logger.LogInformation("Begin database transaction {TransactionId} for {CommandName}", transactionId, commandName);
            var response = await next(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            _logger.LogInformation("Commit database transaction {TransactionId} for {CommandName}", transactionId, commandName);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Rollback database transaction {TransactionId} for {CommandName}", transactionId, commandName);
            throw;
        }
    }
}
