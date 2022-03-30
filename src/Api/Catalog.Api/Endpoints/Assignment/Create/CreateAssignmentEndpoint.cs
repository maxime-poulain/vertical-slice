using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Assignments;
using Catalog.Api.Application.Features.Assignments.AssignTrainerToTraining.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Assignment.Create;

public class AssignmentCreateEndpoint : EndpointBaseAsync.WithRequest<AssignTrainerToTrainingCommand>.WithActionResult<AssignmentDto>
{
    private readonly IMediator _mediator;

    public AssignmentCreateEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("assignment/assign")]
    [SwaggerOperation(
        Summary     = "Assigns a trainer to a training",
        Description = "Assigns a trainer to a training",
        OperationId = "Assign",
        Tags        = new[] { "Assignment" })]
    [ProducesResponseType(typeof(AssignmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<AssignmentDto>> HandleAsync(AssignTrainerToTrainingCommand command, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(command, cancellationToken));
    }
}