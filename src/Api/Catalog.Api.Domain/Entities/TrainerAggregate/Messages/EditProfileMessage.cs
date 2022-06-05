using Catalog.Api.Domain.Entities.Base;
using Catalog.Shared.Enumerations.Trainer;

namespace Catalog.Api.Domain.Entities.TrainerAggregate.Messages;

public record CreateTrainerMessage(
    string FirstName,
    string LastName,
    string? Profession,
    string? Bio,
    string? Email,
    IEnumerable<(SocialNetwork SocialNetwork, string? Url)>? SocialNetworks) : EditProfileMessage(Profession, Bio, Email, SocialNetworks);

public record TrainerCreatedEvent(Trainer trainer) : IDomainEvent
{
    private readonly Trainer _trainer = trainer;

    public Guid Id => _trainer.Id;
}

public record EditProfileMessage(string? Profession, string? Bio, string? Email, IEnumerable<(SocialNetwork SocialNetwork, string? Url)>? SocialNetworks);

public record TrainerProfileEditedEvent(Trainer _trainer) : IDomainEvent
{
    private readonly Trainer _trainer = _trainer;

    public Guid Id => _trainer.Id;
}
