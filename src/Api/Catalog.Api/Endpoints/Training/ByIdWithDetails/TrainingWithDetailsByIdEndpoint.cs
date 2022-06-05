using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Common.Exceptions;
using Catalog.Api.Application.Features.Training.GetDetailedTrainingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.ByIdWithDetails;

public class TrainingWithDetailsByIdEndpoint : EndpointBaseAsync.WithRequest<GetDetailedTrainingByIdQuery>.WithActionResult<DetailedTrainingDto>
{
    private readonly IMediator _mediator;

    public TrainingWithDetailsByIdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("training/{Id:int}/withdetails")]
    [SwaggerOperation(OperationId = "TrainingByIdWithDetails", Tags = new[] { "Training" })]
    [ProducesResponseType(typeof(DetailedTrainingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<DetailedTrainingDto>> HandleAsync([FromRoute] GetDetailedTrainingByIdQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var detailedTrainingDto = await _mediator.Send(request, cancellationToken);

        if (detailedTrainingDto is null)
        {
            throw new EntityNotFoundException(request.Id, typeof(Domain.Entities.TrainingAggregate.Training));
        }

        return detailedTrainingDto;
    }
}
