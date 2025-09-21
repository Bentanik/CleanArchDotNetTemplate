using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class CreateExampleCommandHandler : ICommandHandler<CreateExampleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryBase<ExampleAggregate, Guid> _exampleAggregateRepo;

    public CreateExampleCommandHandler(IUnitOfWork unitOfWork, IRepositoryBase<ExampleAggregate, Guid> exampleAggregateRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
    }

    public async Task<Result> Handle(CreateExampleCommand command, CancellationToken cancellationToken)
    {
        var exampleValueObject = ExampleValueObject.Of(
            exampleValue: command.ExampleValueObjectText
        );

        var exampleStatus = command.ExampleStatus.ToEnum<ExampleEnumDto, ExampleEnum>();

        var exampleAggregate = ExampleAggregate.Create(
            exampleText: command.ExampleText,
            exampleStatus: exampleStatus,
            exampleValueObject: exampleValueObject
        );

        await _exampleAggregateRepo.AddAsync(exampleAggregate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.CreatedSuccessfully.GetMessage().Code, message: ExampleMessages.CreatedSuccessfully.GetMessage().Message);
    }

}