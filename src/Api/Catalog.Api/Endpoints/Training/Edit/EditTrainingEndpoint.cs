using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Training.Edit;
using Catalog.Api.Application.Features.Training.Edit.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.Edit;

public class EditTrainingEndpoint : EndpointBaseAsync.WithRequest<EditTrainingCommand>.WithActionResult<EditedTrainingDto>
{
    private readonly IMediator _mediator;

    public EditTrainingEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("training")]
    [ProducesResponseType(typeof(EditedTrainingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Edits a training",
        Description = "Edits a training",
        OperationId = "EditTraining",
        Tags = new[] { "Training" })]
    public override async Task<ActionResult<EditedTrainingDto>> HandleAsync(EditTrainingCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}
