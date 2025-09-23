using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class CreateExampleItemsCommandHandler : ICommandHandler<CreateExampleItemsCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExampleAggregateCommandRepository _exampleAggregateRepo;
    private readonly ICommandRepository<ExampleItemEntity, Guid> _exampleItemRepo;


    public CreateExampleItemsCommandHandler(IUnitOfWork unitOfWork, IExampleAggregateCommandRepository exampleAggregateRepo, ICommandRepository<ExampleItemEntity, Guid> exampleItemRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
        _exampleItemRepo = exampleItemRepo;
    }

    public async Task<Result> Handle(CreateExampleItemsCommand command, CancellationToken cancellationToken)
    {
        var exampleAggregate = await _exampleAggregateRepo.FindByIdWithItemsAsync(command.ExampleId, cancellationToken);

        if (exampleAggregate == null)
        {
            return Result.Failure(code: AppMessages.NotFound.GetMessage().Code,
                                message: AppMessages.NotFound.GetMessage().Message);
        }

        var duplicates = ValidateBeforeCreate(exampleItems: command.ExampleItems, existingItems: exampleAggregate.Items);

        if (duplicates.Count != 0)
        {
            return Result.Failure(code: ExampleMessages.DuplicateExampleText.GetMessage().Code,
                                message: ExampleMessages.DuplicateExampleText.GetMessage().Message, data: duplicates);
        }

        foreach (var itemDto in command.ExampleItems)
        {
            exampleAggregate.AddItem(itemDto.ExampleText);
            var item = exampleAggregate.Items.Last();
            await _exampleItemRepo.AddAsync(item, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.CreatedSuccessfully.GetMessage().Code, message: ExampleMessages.CreatedSuccessfully.GetMessage().Message);
    }

    private static List<ExampleItemDto> ValidateBeforeCreate(
       IEnumerable<ExampleItemDto> exampleItems,
       IReadOnlyCollection<ExampleItemEntity> existingItems)
    {
        var existingTexts = existingItems.Select(item => item.ExampleText).ToHashSet();

        var duplicateDtos = exampleItems
            .GroupBy(dto => dto.ExampleText)
            .Where(g => g.Count() > 1 || existingTexts.Contains(g.Key))
            .SelectMany(g => g)
            .ToList();

        return duplicateDtos;
    }
}