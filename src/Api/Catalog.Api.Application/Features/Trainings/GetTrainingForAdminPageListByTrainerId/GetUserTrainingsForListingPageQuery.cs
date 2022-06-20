using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Trainings.GetTrainingForAdminPageListByTrainerId;

public class GetUserTrainingsForListingPageQuery : IQuery<IEnumerable<TrainingForAdminPageDto>>
{
    public Guid Id { get; set; }
}

public class GetUserTrainingsForListingPageQueryHandler : IQueryHandler<GetUserTrainingsForListingPageQuery, IEnumerable<TrainingForAdminPageDto>>
{
    private readonly CatalogContext _catalogContext;

    public GetUserTrainingsForListingPageQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<TrainingForAdminPageDto>> Handle(GetUserTrainingsForListingPageQuery request, CancellationToken cancellationToken)
    {
        return await _catalogContext.TrainingAssignment
            .Where(assignment => assignment.TrainerId == request.Id)
            .Select(assignment => assignment.Training)
            .Select(training => new TrainingForAdminPageDto()
            {
                Id          = training!.Id,
                Title       = training.Title,
                Description = training.Description,
                Topics      = training.Topics.Select(topic => topic.Topic)
            }).ToListAsync(cancellationToken);
    }
}
