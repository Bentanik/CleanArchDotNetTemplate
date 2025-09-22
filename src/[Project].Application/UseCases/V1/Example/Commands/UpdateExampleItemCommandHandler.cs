using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class UpdateExampleItemCommandHandler : ICommandHandler<UpdateExampleItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExampleAggregateRepository _exampleAggregateRepo;
    private readonly IRepositoryBase<ExampleItemEntity, Guid> _exampleItemRepo;

    public UpdateExampleItemCommandHandler(IUnitOfWork unitOfWork, IExampleAggregateRepository exampleAggregateRepo, IRepositoryBase<ExampleItemEntity, Guid> exampleItemRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
        _exampleItemRepo = exampleItemRepo;
    }

    public async Task<Result> Handle(UpdateExampleItemCommand command, CancellationToken cancellationToken)
    {
        var (exampleAggregate, isDuplicateText) = await _exampleAggregateRepo.FindAggregateWithItemAndCheckDuplicateAsync(
            id: command.ExampleId,
            exampleItemId: command.ExampleItemId,
            exampleText: command.ExampleItemText,
            cancellationToken: cancellationToken);

        if (exampleAggregate == null)
        {
            return Result.Failure(code: AppMessages.NotFound.GetMessage().Code,
                                message: AppMessages.NotFound.GetMessage().Message);
        }

        if (isDuplicateText == true)
        {
            return Result.Failure(code: ExampleMessages.DuplicateExampleText.GetMessage().Code,
                                message: ExampleMessages.DuplicateExampleText.GetMessage().Message, data: command.ExampleItemText);
        }

        exampleAggregate.UpdateItem(
            itemId: command.ExampleItemId,
            exampleText: command.ExampleItemText
        );

        var exampleItem = exampleAggregate.Items.Last();
        await _exampleItemRepo.UpdateAsync(exampleItem, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.UpdatedSuccessfully.GetMessage().Code, message: ExampleMessages.UpdatedSuccessfully.GetMessage().Message);
    }
}