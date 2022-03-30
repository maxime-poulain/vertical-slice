using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Features.Trainer.CreateTrainer;
using Catalog.Api.Application.Features.Trainer.CreateTrainer.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Endpoints.Trainer.Create;

public class TrainerCreationEndpoint : EndpointBaseAsync.WithRequest<CreateTrainerCommand>.WithActionResult<TrainerCreationResult>
{
    private readonly IMediator _mediator;

    public TrainerCreationEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("trainer")]
    [SwaggerOperation(Summary = "Creates a trainer",
        Description = "Creates a trainer",
        OperationId = "CreateTrainer",
        Tags = new []{ "Trainer" })]
    [ProducesResponseType(typeof(TrainerCreationResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public override async Task<ActionResult<TrainerCreationResult>> HandleAsync(CreateTrainerCommand request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request, cancellationToken));
    }
}