using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Assignments;
using Catalog.Api.Application.Features.Assignments.GetAllAssignments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Assignment.All;

public class AssignmentGetAll : EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<AssignmentDto>>
{
    private readonly IMediator _mediator;

    public AssignmentGetAll(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("assignment")]
    [SwaggerOperation(
        Summary = "Gets every assignments",
        Description = "Gets every assignments",
        OperationId = "AllAssignments",
        Tags = new[] { "Assignment" })]
    [ProducesResponseType(typeof(IEnumerable<AssignmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<IEnumerable<AssignmentDto>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAllAssignmentsQuery(), cancellationToken));
    }
}
