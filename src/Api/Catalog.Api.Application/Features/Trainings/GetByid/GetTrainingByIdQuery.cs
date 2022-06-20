using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Trainings.GetByid;

public class GetTrainingByIdQuery : IQuery<TrainingByIdDto?>
{
    public Guid Id { get; set; }
}

public class GetTrainingByIdQueryHandler : IQueryHandler<GetTrainingByIdQuery, TrainingByIdDto?>
{
    private readonly CatalogContext _catalogContext;

    public GetTrainingByIdQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<TrainingByIdDto?> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await _catalogContext.Training.FindAsync(request.Id);

        return training is not null ?  new TrainingByIdDto()
        {
            Title                   = training.Title,
            Description             = training.Description,
            Id                      = training.Id
        } : null;
    }
}
