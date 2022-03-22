using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainingId;

public class GetAssignmentsByTrainingIdQuery : IQuery<IEnumerable<AssignmentDto>>
{
    public int TrainingId { get; init; }

    public GetAssignmentsByTrainingIdQuery()
    {
    }
}

public class GetAssignmentsByTrainingIdQueryHandler : IQueryHandler<GetAssignmentsByTrainingIdQuery, IEnumerable<AssignmentDto>>
{
    private readonly CatalogContext _catalogContext;

    public GetAssignmentsByTrainingIdQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignmentsByTrainingIdQuery command, CancellationToken cancellationToken)
    {
        return await _catalogContext.TrainingAssignment
            .Where(assignment  => assignment.TrainingId == command.TrainingId)
            .Select(assignment => new AssignmentDto()
            {
                TrainerId  = assignment.TrainerId,
                TrainingId = assignment.TrainingId
            }).ToListAsync(cancellationToken);
    }
}
