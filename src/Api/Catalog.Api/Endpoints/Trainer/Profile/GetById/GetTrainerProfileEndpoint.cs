using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Trainer.GetTrainerProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Trainer.Profile.GetById;

public class GetTrainerProfileEndpoint : EndpointBaseAsync.WithRequest<GetTrainerProfileQuery>.WithActionResult<TrainerProfileDto>
{
    private readonly IMediator _mediator;

    public GetTrainerProfileEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/trainer/{Id}/profile")]
    [ProducesResponseType(typeof(TrainerProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Gets the profile of a given trainer",
        Description = "Gets the profile of a given trainer",
        OperationId = "GetTrainerProfileById",
        Tags = new[] { "Trainer" })]
    public override async Task<ActionResult<TrainerProfileDto>> HandleAsync([FromRoute] GetTrainerProfileQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var profile = await _mediator.Send(request, cancellationToken);

        if (profile is null)
        {
            return NotFound();
        }

        return Ok(profile);
    }
}
