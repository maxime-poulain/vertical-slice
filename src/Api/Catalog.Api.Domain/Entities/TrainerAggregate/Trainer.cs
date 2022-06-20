using Ardalis.GuardClauses;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Entities.TrainerAggregate.Messages;
using Catalog.Api.Domain.Entities.TrainingAggregate;
using Catalog.Api.Domain.Extensions;
using Catalog.Shared.Enumerations.Trainer;

namespace Catalog.Api.Domain.Entities.TrainerAggregate;

public class Trainer : Entity, IEntity
{
    private string _firstname = null!;

    public string Firstname
    {
        get  => _firstname;
        init => ChangeFirstname(value);
    }

    private string _lastname = null!;

    public string Lastname
    {
        get  => _lastname;
        init => ChangeLastname(value);
    }

    private string _bio = null!;

    public string Bio
    {
        get  => _bio;
        init => ChangeBio(value);
    }

    private string? _profession;

    public string? Profession
    {
        get => _profession;
        private set => ChangeProfession(value);
    }

    private string? _email;

    public string? Email
    {
        get => _email;
        private set => ChangeEmail(value);
    }

    public string Hash { get; private set; }
    public string Salt { get; private set; }

    public void SetHashAndSalt(string hash, string password)
    {
        Hash = Guard.Against.NullOrWhiteSpace(hash);
        Salt = Guard.Against.NullOrWhiteSpace(password);
    }

    private readonly List<TrainingAssignment> _trainingAssignments;

    public IReadOnlyList<TrainingAssignment> TrainingAssignments => _trainingAssignments.AsReadOnly();

    private readonly List<TrainerSocialNetwork> _socialNetworks = new();

    public IReadOnlyCollection<TrainerSocialNetwork> SocialNetworks => _socialNetworks;

    public void ChangeBio(string? bio)
    {
        _bio = Guard.Against.NullOrEmpty(bio, nameof(bio));
        _bio = Guard.Against.Between(bio, 30, 500, nameof(bio));
    }

    private Trainer()
    {
        _trainingAssignments = new List<TrainingAssignment>();
    }

    public static Trainer Create(CreateTrainerMessage message)
    {
        var trainer = new Trainer();
        trainer.ChangeName(message.FirstName, message.LastName);
        trainer.EditProfile(message);
        trainer.DomainEvents.Add(new TrainerCreatedEvent(trainer));
        return trainer;
    }

    public void AssignTraining(TrainingAssignment assignment)
    {
        _trainingAssignments.Add(assignment);
    }

    public void ChangeFirstname(string firstname)
    {
        _firstname = Guard.Against.NullOrWhiteSpace(firstname, nameof(firstname));
    }

    public void ChangeLastname(string lastname)
    {
        _lastname = Guard.Against.NullOrWhiteSpace(lastname, nameof(lastname));
    }

    public void ChangeName(string firstname, string lastname)
    {
        ChangeFirstname(firstname);
        ChangeLastname(lastname);
    }

    public void ChangeProfession(string? title)
    {
        _profession = title;
    }

    public void ChangeEmail(string? email)
    {
        _email = email;
    }

    private void ChangeSocialNetworks(IEnumerable<(SocialNetwork SocialNetwork, string? Url)>? socialNetworks)
    {
        _socialNetworks.Clear();
        var socialNetworksToAdd = socialNetworks?
            .Where(personalUrlToSocialNetwork => !string.IsNullOrWhiteSpace(personalUrlToSocialNetwork.Url))
            .DistinctBy(social => social.SocialNetwork)
            .Select(personalUrlToSocialNetwork =>
                new TrainerSocialNetwork(this, personalUrlToSocialNetwork.SocialNetwork, personalUrlToSocialNetwork.Url))
            ?? Array.Empty<TrainerSocialNetwork>();

        _socialNetworks.AddRange(socialNetworksToAdd);
    }

    public void EditProfile(EditProfileMessage message)
    {
        ChangeProfession(message.Profession);
        ChangeBio(message.Bio);
        ChangeEmail(message.Email);
        ChangeSocialNetworks(message.SocialNetworks);

        // We don't want to trigger the edited event when the underlying trainer doesn't exist yet.
        if (Id != default)
        {
            DomainEvents.Add(new TrainerProfileEditedEvent(this));
        }
    }
}
