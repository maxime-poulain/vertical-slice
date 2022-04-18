using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Trainer.Delete.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Trainer.Delete;

public class DeleteTrainerEndpoint : EndpointBaseAsync.WithRequest<DeleteTrainerCommand>.WithActionResult
{
    private readonly IMediator _mediator;

    public DeleteTrainerEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete($"trainer/{{{nameof(DeleteTrainerCommand.Id)}:int}}")]
    [SwaggerOperation(Summary = "Deletes a trainer by its id",
        Description = "Deletes a trainer by its id",
        OperationId = "DeleteTrainer",
        Tags = new []{ "Trainer" })]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteTrainerCommand command, CancellationToken cancellationToken = default)
    {
        var trainerWasFound = await _mediator.Send(command, cancellationToken);

        if (trainerWasFound)
        {
            return NoContent();
        }

        return NotFound();
    }
}
