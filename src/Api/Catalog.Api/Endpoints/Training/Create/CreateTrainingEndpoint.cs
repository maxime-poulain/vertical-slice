using Catalog.Api.Application.Features.Training.Create;
using Catalog.Api.Application.Features.Training.Create.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.Create;

public class TrainingCreationEndpoint : Ardalis.ApiEndpoints.EndpointBaseAsync.WithRequest<CreateTrainingCommand>.WithActionResult<CreatedTrainingDto>
{
    private readonly IMediator _mediator;

    public TrainingCreationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("training")]
    [ProducesResponseType(typeof(CreatedTrainingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary     = "Creates a new training",
        Description = "Creates a new training",
        OperationId = "CreateTraining",
        Tags        = new []{ "Training" })
    ]
    public override async Task<ActionResult<CreatedTrainingDto>> HandleAsync(CreateTrainingCommand command, CancellationToken cancellationToken = default)
    {
        return StatusCode(StatusCodes.Status201Created, await _mediator.Send(command, cancellationToken));
    }
}