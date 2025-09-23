using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class DeleteExampleCommandHandler : ICommandHandler<DeleteExampleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExampleAggregateCommandRepository _exampleAggregateRepo;

    public DeleteExampleCommandHandler(IUnitOfWork unitOfWork, IExampleAggregateCommandRepository exampleAggregateRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
    }

    public async Task<Result> Handle(DeleteExampleCommand command, CancellationToken cancellationToken)
    {
        var exampleAggregate = await _exampleAggregateRepo.FindSingleAsync
        (predicate: ex => ex.Id == command.ExampleId, cancellationToken: cancellationToken);

        if (exampleAggregate == null)
        {
            var error = new Error(code: AppMessages.NotFound.GetMessage().Code,
                                message: AppMessages.NotFound.GetMessage().Message);

            return Result.Failure([error]);
        }

        exampleAggregate.Delete();
        await _exampleAggregateRepo.RemoveAsync(exampleAggregate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.DeletedSuccessfully.GetMessage().Code, message: ExampleMessages.DeletedSuccessfully.GetMessage().Message);
    }
}