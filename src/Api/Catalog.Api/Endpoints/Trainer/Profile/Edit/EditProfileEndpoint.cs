using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Trainer.EditProfile.Command;
using Catalog.Api.Application.Features.Trainer.GetTrainerProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Trainer.Profile.Edit;

public class EditProfileEndpoint : EndpointBaseAsync.WithRequest<EditTrainerProfileCommand>.WithActionResult<TrainerProfileDto>
{
    private readonly IMediator _mediator;

    public EditProfileEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/trainer/profile")]
    [ProducesResponseType(typeof(TrainerProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Edits the profile of a given trainer",
        Description = "Edits the profile of a given trainer",
        OperationId = "EditTrainerProfileById",
        Tags = new[] { "Trainer" })]
    public override async Task<ActionResult<TrainerProfileDto>> HandleAsync(EditTrainerProfileCommand request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return Ok(await _mediator.Send(new GetTrainerProfileQuery(request.Id), cancellationToken));
    }
}
