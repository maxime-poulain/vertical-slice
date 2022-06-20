using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities.TrainerAggregate;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Trainers.GetTrainerByEmail;

public class GetTrainerByEmailQuery : IQuery<Trainer>
{
    public string Email { get; set; } = null!;
}

public class GetTrainerByEmailQueryHandler : IQueryHandler<GetTrainerByEmailQuery, Trainer>
{
    private readonly CatalogContext _catalogContext;

    public GetTrainerByEmailQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }
    public Task<Trainer> Handle(GetTrainerByEmailQuery query, CancellationToken cancellationToken)
    {
        var trainer = _catalogContext.Trainer.Single(trainer => trainer.Email == query.Email);
    }
}
