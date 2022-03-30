using Ardalis.GuardClauses;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Extensions;
using Catalog.Shared.Enumerations.Trainer;

namespace Catalog.Api.Domain.Entities;

public class Trainer : Entity, IEntity
{
    private string _firstname;

    public string Firstname
    {
        get  => _firstname;
        init => ChangeFirstname(value);
    }

    private string _lastname;

    public string Lastname
    {
        get  => _lastname;
        init => ChangeLastname(value);
    }

    private string _bio;

    public string Bio
    {
        get  => _bio;
        init => ChangeBio(value);
    }

    private void ChangeBio(string bio)
    {
        Guard.Against.MaxLength(bio, 500, nameof(bio));
        _bio = Guard.Against.MinLength(bio, 30, nameof(bio));
    }

    private TrainerSkillLevel _skillLevel;

    public TrainerSkillLevel SkillLevel
    {
        get => _skillLevel;
        init => ChangeSkillLevel(value);
    }

    private readonly List<TrainingAssignment> _trainingAssignments;

    public IReadOnlyList<TrainingAssignment> TrainingAssignments => _trainingAssignments.AsReadOnly();

    public Trainer(string firstname, string lastname, string bio, TrainerSkillLevel skillLevel)
    {
        Firstname  = firstname;
        Lastname   = lastname;
        SkillLevel = skillLevel;
        Bio        = bio;

        _trainingAssignments = new List<TrainingAssignment>();
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

    public void ChangeSkillLevel(TrainerSkillLevel level)
    {
        _skillLevel = level;
    }
}