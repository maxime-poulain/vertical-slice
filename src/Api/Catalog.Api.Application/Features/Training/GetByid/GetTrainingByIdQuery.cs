﻿using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Enumerations.Training;
using Catalog.Api.EfCore.Context;

namespace Catalog.Api.Application.Features.Training.GetById;

public class GetTrainingByIdQuery : IQuery<TrainingByIdDto?>
{
    public int Id { get; set; }
}

public class GetTrainingByIdQueryHandler : IQueryHandler<GetTrainingByIdQuery, TrainingByIdDto?>
{
    private readonly CatalogContext _catalogContext;

    public GetTrainingByIdQueryHandler(CatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task<TrainingByIdDto?> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await _catalogContext.Training.FindAsync(request.Id);

        return training is not null ?  new TrainingByIdDto()
        {
            Title                   = training.Title,
            Description             = training.Description,
            Id                      = training.Id,
            TrainingTypeDescription = TrainingType.FromValue(training.TrainingType.Value).Name,
            TrainingTypeId          = training.TrainingType.Value
        } : null;
    }
}