using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Features.Training.Common.CreateEdit;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Training.Edit.Command;

public class EditTrainingCommand : CreateEditTrainingCommonCommand<EditedTrainingDto>
{
    public int Id { get; set; }
}

public class EditTrainingCommandHandler : CreateEditTrainingCommonCommandHandler<EditTrainingCommand, EditedTrainingDto>
{

    public EditTrainingCommandHandler(CatalogContext catalogContext) : base(catalogContext)
    {
    }

    protected override async Task<Domain.Entities.Training> GetOrMakeTrainingAsync(EditTrainingCommand command)
    {
        return await CatalogContext.Training
                   .Include(training => training.Topics)
                   .Include(training => training.VatJustifications)
                   .Include(training => training.Attendances)
                   .Include(training => training.Audiences)
                   .FirstOrDefaultAsync(x => x.Id == command.Id)
                       ?? throw new EntityNotFoundException(command.Id, typeof(Domain.Entities.Training));
    }

    protected override EditedTrainingDto MakeResult(Domain.Entities.Training training)
    {
        return new EditedTrainingDto()
        {
            Title                   = training.Title,
            Description             = training.Description,
            Goal                    = training.Goal,
            TrainingId              = training.Id,
            TrainingTypeDescription = training.TrainingType.Name,
            TrainingTypeId          = training.TrainingType.Value
        };
    }

    protected override IDomainEvent DomainEventForCurrentOperation(Domain.Entities.Training training)
    {
        return new TrainingEditedEvent(training);
    }
}