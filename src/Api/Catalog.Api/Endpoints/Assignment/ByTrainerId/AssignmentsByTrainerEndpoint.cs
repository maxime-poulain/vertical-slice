using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Assignments;
using Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Assignment.ByTrainerId;

public class AssignmentsByTrainerEndpoint : EndpointBaseAsync.WithRequest<GetAssignmentsByTrainerIdQuery>.WithActionResult<IEnumerable<AssignmentDto>>
{
    private readonly IMediator _mediator;

    public AssignmentsByTrainerEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("trainer/{Id}/assignment")]
    [SwaggerOperation(Summary = "Retrieves all assignments of a given trainer",
        Description = "Retrieves all assignments of a given trainer",
        OperationId = "AssignmentByTrainerId",
        Tags = new[] { "Assignment" })]
    [ProducesResponseType(typeof(IEnumerable<AssignmentDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<IEnumerable<AssignmentDto>>> HandleAsync([FromRoute] GetAssignmentsByTrainerIdQuery request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}