using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Training.Delete.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.Delete;

public class TrainingDeletionEndpoint : EndpointBaseAsync.WithRequest<DeleteTrainingCommand>.WithActionResult
{
    private readonly IMediator _mediator;

    public TrainingDeletionEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("training/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary     = "Deletes a training",
        Description = "Deletes a training by a given id",
        OperationId = "Training.Delete",
        Tags        = new []{ "Training" })]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteTrainingCommand command, CancellationToken cancellationToken = default)
    {
        // The command returns true if a deletion was done.
        if (await _mediator.Send(command, cancellationToken))
        {
            return NoContent();
        }

        // False otherwise, which means that the Training was not existing inside the database.
        return NotFound();
    }
}