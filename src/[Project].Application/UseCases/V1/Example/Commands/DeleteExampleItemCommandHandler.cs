using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class DeleteExampleItemCommandHandler : ICommandHandler<DeleteExampleItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExampleAggregateCommandRepository _exampleAggregateRepo;
    private readonly ICommandRepository<ExampleItemEntity, Guid> _exampleItemRepo;

    public DeleteExampleItemCommandHandler(IUnitOfWork unitOfWork, IExampleAggregateCommandRepository exampleAggregateRepo, ICommandRepository<ExampleItemEntity, Guid> exampleItemRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
        _exampleItemRepo = exampleItemRepo;
    }

    public async Task<Result> Handle(DeleteExampleItemCommand command, CancellationToken cancellationToken)
    {
        var (exampleAggregate, _) = await _exampleAggregateRepo.FindAggregateWithItemAndCheckDuplicateAsync(
            id: command.ExampleId,
            exampleItemId: command.ExampleItemId,
            exampleText: null,
            cancellationToken: cancellationToken);
        
        if (exampleAggregate == null)
        {
            return Result.Failure(code: AppMessages.NotFound.GetMessage().Code,
                                message: AppMessages.NotFound.GetMessage().Message);
        }

        exampleAggregate.DeleteItem(
            itemId: command.ExampleItemId
        );

        var exampleItem = exampleAggregate.Items.Last();
        await _exampleItemRepo.RemoveAsync(exampleItem, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.DeletedSuccessfully.GetMessage().Code, message: ExampleMessages.DeletedSuccessfully.GetMessage().Message);
    }
}