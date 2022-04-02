using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Training.GetDetailedTrainingById;

public class GetDetailedTrainingByIdQuery : IQuery<DetailedTrainingDto?>
{
    public int Id { get; set; }
}

public class GetDetailedTrainingByIdQueryHandler : IQueryHandler<GetDetailedTrainingByIdQuery, DetailedTrainingDto?>
{
    private readonly CatalogContext _catalogContext;

    public GetDetailedTrainingByIdQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<DetailedTrainingDto?> Handle(GetDetailedTrainingByIdQuery query, CancellationToken cancellationToken)
    {
        return await _catalogContext.Training
            .Include(training => training.Topics)
            .Include(training => training.Assignments)
            .Include(training => training.Attendances)
            .Include(training => training.VatJustifications)
            .Select(training => new DetailedTrainingDto()
            {
                Title                = training.Title,
                Id                   = training.Id,
                Topics               = training.Topics.Select(topic => topic.Topic.Value),
                Description          = training.Description,
                Attendances          = training.Attendances.Select(attendance => attendance.Attendance.Value),
                Audiences            = training.Audiences.Select(audience => audience.Audience.Value),
                Goal                 = training.Goal,
                VatJustifications    = training.VatJustifications.Select(trainingVatJustification => trainingVatJustification.VatJustification.Value)
            }).FirstOrDefaultAsync(training => training.Id == query.Id, cancellationToken);
    }
}
