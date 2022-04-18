using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Assignments.GetAllAssignments;

public class GetAllAssignmentsQuery : IQuery<IEnumerable<AssignmentDto>>
{
}

public class GetAllAssignmentsQueryHandler : IQueryHandler<GetAllAssignmentsQuery, IEnumerable<AssignmentDto>>
{
    private readonly CatalogContext _catalogContext;

    public GetAllAssignmentsQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<AssignmentDto>> Handle(GetAllAssignmentsQuery request, CancellationToken cancellationToken)
    {
        return await _catalogContext.TrainingAssignment
            .Select(assignment => new AssignmentDto()
            {
                TrainerId  = assignment.TrainerId,
                TrainingId = assignment.TrainingId
            })
            .ToListAsync(cancellationToken);
    }
}
