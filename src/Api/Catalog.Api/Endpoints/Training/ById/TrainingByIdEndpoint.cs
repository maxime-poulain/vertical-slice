using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Training.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.ById;

public class TrainingByIdEndpoint : EndpointBaseAsync.WithRequest<GetTrainingByIdQuery>.WithActionResult<TrainingByIdDto>
{
    private readonly IMediator _mediator;

    public TrainingByIdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("training/{Id:int}")]
    [SwaggerOperation(
        Summary     = "Gets a training by its id",
        Description = "Gets a training by its id",
        OperationId = "TrainingById",
        Tags        = new[] { "Training" })
    ]
    [ProducesResponseType(typeof(TrainingByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<TrainingByIdDto>> HandleAsync([FromRoute] GetTrainingByIdQuery request, CancellationToken cancellationToken = default)
    {
        var training = await _mediator.Send(request, cancellationToken);

        return Ok(training);
    }
}
