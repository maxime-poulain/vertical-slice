using Catalog.Api.Domain.CQS;

namespace Catalog.Api.Application.Features.Training.Common.CreateEdit;

public abstract class CreateEditTrainingCommonCommand<TResponse> : ICommand<TResponse>
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Goal { get; set; }

    public List<int>? Topics { get; set; }

    public List<int>? Attendances { get; set; }

    public List<int>? VatJustifications { get; set; }

    public List<int>? Audiences { get; set; }
}
