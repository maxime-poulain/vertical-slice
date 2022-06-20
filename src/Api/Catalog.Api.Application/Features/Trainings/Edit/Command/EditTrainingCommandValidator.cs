using Catalog.Api.Application.Features.Trainings.Common.CreateEdit;
using Catalog.Api.EfCore.Context;
using Catalog.Api.EfCore.Extensions;
using FluentValidation;

namespace Catalog.Api.Application.Features.Trainings.Edit.Command;

public class EditTrainingCommandValidator : CreateEditTrainingCommonValidator<EditTrainingCommand, EditedTrainingDto>
{
    public EditTrainingCommandValidator(CatalogContext catalogContext) : base()
    {
        RuleFor(command => command.Id)
            .MustAsync((id,  token) => catalogContext.Training.ExistsAsync(id, token));
    }
}
