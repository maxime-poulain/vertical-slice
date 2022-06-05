using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainerId;

public class GetAssignmentsByTrainerIdQuery : IQuery<IEnumerable<AssignmentDto>>
{
    /// <summary>
    /// Trainer's id.
    /// </summary>
    public Guid Id { get; set; }
}

public class GetAssignmentsByTrainerIdQueryHandler : IQueryHandler<GetAssignmentsByTrainerIdQuery, IEnumerable<AssignmentDto>>
{
    private readonly CatalogContext _catalogContext;

    public GetAssignmentsByTrainerIdQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<IEnumerable<AssignmentDto>> Handle(GetAssignmentsByTrainerIdQuery command, CancellationToken cancellationToken)
    {
        return await _catalogContext.TrainingAssignment
            .Where(assignment  => assignment.TrainerId == command.Id)
            .Select(assignment => new AssignmentDto()
            {
                TrainerId  = assignment.TrainerId,
                TrainingId = assignment.TrainingId
            }).ToListAsync(cancellationToken);
    }
}
