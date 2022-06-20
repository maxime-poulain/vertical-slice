using Catalog.Api.Application.Features.Trainers.Common.Dtos;
using Catalog.Api.Domain.CQS;
using Catalog.Api.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Application.Features.Trainers.GetTrainerProfile;

public class GetTrainerProfileQuery : IQuery<TrainerProfileDto?>
{
    public Guid Id { get; init; }

    public GetTrainerProfileQuery()
    {
    }

    public GetTrainerProfileQuery(Guid id)
    {
        Id = id;
    }
}

public class GetTrainerProfileQueryHandler : IQueryHandler<GetTrainerProfileQuery, TrainerProfileDto?>
{
    private readonly CatalogContext _catalogContext;

    public GetTrainerProfileQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public Task<TrainerProfileDto?> Handle(GetTrainerProfileQuery query, CancellationToken cancellationToken)
    {
        return _catalogContext.Trainer
            .Select(trainer => new TrainerProfileDto()
            {
                Id             = trainer.Id,
                Email          = trainer.Email,
                Firstname      = trainer.Firstname,
                Lastname       = trainer.Lastname,
                Bio            = trainer.Bio,
                Profession     = trainer.Profession,
                SocialNetworks = trainer.SocialNetworks.ToDtos()
            }).FirstOrDefaultAsync(trainer => trainer.Id == query.Id, cancellationToken);
    }
}
