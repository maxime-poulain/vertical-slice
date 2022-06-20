using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Trainings.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.All;

public class TrainingGetAllEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<TrainingDto>>
{
    private readonly IMediator _mediator;

    public TrainingGetAllEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("training")]
    [ProducesResponseType(typeof(IEnumerable<TrainingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary     = "Gets every trainings",
        Description = "Gets every trainings",
        OperationId = "AllTrainings",
        Tags        = new[] { "Training" })
    ]
    public override async Task<ActionResult<IEnumerable<TrainingDto>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var trainings = await _mediator.Send(new GetAllTrainingsQuery(), cancellationToken);
        return Ok(trainings);
    }
}
