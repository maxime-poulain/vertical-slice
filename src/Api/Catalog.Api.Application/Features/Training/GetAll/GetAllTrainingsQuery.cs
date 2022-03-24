using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Training.GetAll;

public class GetAllTrainingsQuery : IQuery<IEnumerable<TrainingDto>>
{
}
public class GetAllTrainingsQueryHandler : IQueryHandler<GetAllTrainingsQuery, IEnumerable<TrainingDto>>
{
    private readonly CatalogContext _catalogContext;

    public GetAllTrainingsQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<TrainingDto>> Handle(GetAllTrainingsQuery request, CancellationToken cancellationToken)
    {
        return await _catalogContext.Training
            .Select(training => new TrainingDto()
            {
                Id           = training.Id,
                Title        = training.Title,
                Description  = training.Description,
            }).ToListAsync(cancellationToken);
    }
}