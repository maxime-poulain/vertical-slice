using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Assignments;
using Catalog.Api.Application.Features.Assignments.GetAssignmentsByTrainingId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Assignment.ByTrainingId;

public class AssignmentByTrainingIdEndpoint : EndpointBaseAsync.WithRequest<GetAssignmentsByTrainingIdQuery>.WithActionResult<IEnumerable<AssignmentDto>>
{
    private readonly IMediator _mediator;

    public AssignmentByTrainingIdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("training/{Id:guid}/assignment")]
    [SwaggerOperation(
        Summary     = "Gets every assignments of a given training",
        Description = "Gets every assignments of a given training",
        OperationId = "AssignmentByTrainingId",
        Tags        = new [] { "Assignment" })]
    [ProducesResponseType(typeof(IEnumerable<AssignmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<IEnumerable<AssignmentDto>>> HandleAsync([FromRoute] GetAssignmentsByTrainingIdQuery request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
