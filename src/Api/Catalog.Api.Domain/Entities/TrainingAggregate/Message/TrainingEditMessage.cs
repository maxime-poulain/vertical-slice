namespace Catalog.Api.Domain.Entities.TrainingAggregate.Message;

public abstract class TrainingEditMessage
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Goal { get; set; }

    public List<int>? Topics { get; set; }

    public List<int>? Attendances { get; set; }

    public List<int>? VatJustifications { get; set; }

    public List<int>? Audiences { get; set; }
}