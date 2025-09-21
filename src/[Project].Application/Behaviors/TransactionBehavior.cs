namespace _Project_.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _uow;
    private const string CommandSuffix = "Command";
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(IUnitOfWork uow, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TransactionBehavior<TRequest, TResponse>.IsCommand())
                return await next();

            await _uow.BeginTransactionAsync(cancellationToken);

            var response = await next();

            await _uow.SaveChangesAsync(cancellationToken);
            await _uow.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Transaction failed for {RequestType}", typeof(TRequest).Name);
            throw;
        }
    }

    private static bool IsCommand()
        => typeof(TRequest).Name.EndsWith(CommandSuffix);
}