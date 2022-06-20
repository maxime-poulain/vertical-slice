namespace Catalog.Api.Application.Features.Trainings.GetByid;

public class TrainingByIdDto
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }
}
