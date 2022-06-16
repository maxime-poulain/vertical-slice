using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Training.GetTrainingForAdminPageListByTrainerId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Training.AllForUserListingPage;

public class TrainingByTrainerIdForListAdminPageEndpoint : EndpointBaseAsync.WithRequest<GetUserTrainingsForListingPageQuery>.WithActionResult<IEnumerable<TrainingForAdminPageDto>>
{
    private readonly IMediator _mediator;

    public TrainingByTrainerIdForListAdminPageEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/trainer/{Id}/foruserlistingpage")]
    [SwaggerOperation(Summary = "Get all training of a trainer for the admin page", OperationId = "TrainingForAdminPageByTrainerId", Tags = new []{ "Training" })]
    public override async Task<ActionResult<IEnumerable<TrainingForAdminPageDto>>> HandleAsync([FromRoute] GetUserTrainingsForListingPageQuery query, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(query, cancellationToken));
    }
}
