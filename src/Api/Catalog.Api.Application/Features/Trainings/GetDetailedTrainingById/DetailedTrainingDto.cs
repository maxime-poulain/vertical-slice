using Catalog.Api.Application.Features.Trainings.Edit;

namespace Catalog.Api.Application.Features.Trainings.GetDetailedTrainingById;

public record DetailedTrainingDto : EditedTrainingDto
{
    public IEnumerable<int>? Attendances { get; set; }

    public IEnumerable<int>? Audiences { get; set; }

    public IEnumerable<int>? Topics { get; set; }

    public IEnumerable<int>? VatJustifications { get; set; }
}
