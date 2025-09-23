using _Project_.Contracts.UseCases.ExampleUseCase.Commands;

namespace _Project_.Application.UseCases.V1.Example.Commands;

public sealed class UpdateExampleCommandHandler : ICommandHandler<UpdateExampleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExampleAggregateCommandRepository _exampleAggregateRepo;

    public UpdateExampleCommandHandler(IUnitOfWork unitOfWork, IExampleAggregateCommandRepository exampleAggregateRepo)
    {
        _unitOfWork = unitOfWork;
        _exampleAggregateRepo = exampleAggregateRepo;
    }

    public async Task<Result> Handle(UpdateExampleCommand command, CancellationToken cancellationToken)
    {
        var (exampleAggregate, isDuplicateText) = await _exampleAggregateRepo.FindByIdAndCheckDuplicateExampleTextAsync(id: command.ExampleId, exampleText: command.ExampleText
        , cancellationToken: cancellationToken);

        if (exampleAggregate == null)
        {
            var error = new Error(code: AppMessages.NotFound.GetMessage().Code,
                                message: AppMessages.NotFound.GetMessage().Message);

            return Result.Failure([error]);
        }

        if (isDuplicateText == true)
        {
            var error = new Error<string>(code: ExampleMessages.DuplicateExampleText.GetMessage().Code,
                                message: ExampleMessages.DuplicateExampleText.GetMessage().Message, data: command.ExampleText);

            return Result.Failure([error]);
        }

        ExampleValueObject? exampleValueObject = null;
        if (command.ExampleValueObjectText != null)
        {
            exampleValueObject = ExampleValueObject.Of(command.ExampleValueObjectText);
        }

        ExampleStatusEnum? exampleStatus = null;
        if (command.ExampleStatus.HasValue)
        {
            exampleStatus = command.ExampleStatus.ToEnumNullable<ExampleStatusEnumDto, ExampleStatusEnum>();
        }

        exampleAggregate.Update(
            exampleText: command.ExampleText,
            exampleValueObject: exampleValueObject,
            exampleStatus: exampleStatus
        );

        await _exampleAggregateRepo.UpdateAsync(exampleAggregate, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(code: ExampleMessages.UpdatedSuccessfully.GetMessage().Code, message: ExampleMessages.UpdatedSuccessfully.GetMessage().Message);
    }
}