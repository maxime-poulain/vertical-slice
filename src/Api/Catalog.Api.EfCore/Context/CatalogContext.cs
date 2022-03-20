using Catalog.Api.Domain.Entities;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.EfCore.Configurations;
using Catalog.Api.EfCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.EfCore.Context;

public class CatalogContext : DbContext
{
    private readonly IMediator _mediator;

    public DbSet<Trainer> Trainer { get; set; } = null!;

    public DbSet<Training> Training { get; set; } = null!;

    public DbSet<TrainingAssignment> TrainingAssignment { get; set; } = null!;

    public CatalogContext() : base()
    {
    }

    public CatalogContext(DbContextOptions<CatalogContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainingConfiguration).Assembly);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return SaveChangesAsync(acceptAllChangesOnSuccess).GetAwaiter().GetResult();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateEntitiesDates();
        var ret = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        await PublishEventsAsync(cancellationToken);

        return ret;
    }

    private void UpdateEntitiesDates()
    {
        ChangeTracker.Entries<IEntity>().UpdateEntitiesAuditableDates();
    }

    private async Task PublishEventsAsync(CancellationToken cancellationToken)
    {
        // Publish Domain events.
        var events = ChangeTracker
            .Entries<IEntity>()
            .SelectMany(entityEntry => entityEntry.Entity.DomainEvents);

        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}