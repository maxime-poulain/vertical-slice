using Ardalis.GuardClauses;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Enumerations.Training;
using Catalog.Api.Domain.Extensions;

namespace Catalog.Api.Domain.Entities;

public class Training : Entity, IEntity
{
    private string _title;

    public string Title
    {
        get  => _title;
        init => ChangeTitle(value);
    }

    private string _description;

    public string Description
    {
        get  => _description;
        init => ChangeDescription(value);
    }

    private string _goal;

    public string Goal
    {
        get  => _goal;
        init => ChangeGoal(value);
    }

    private TrainingType _trainingType;

    public TrainingType TrainingType
    {
        get  => _trainingType;
        init => ChangeType(value);
    }

    private readonly List<TrainingAssignment> _assignments;

    public IReadOnlyList<TrainingAssignment> Assignments => _assignments.AsReadOnly();


    private Training()
    {
        _assignments = new List<TrainingAssignment>();
    }

    public Training(string title, string description, string goal, TrainingType trainingType) : this()
    {
        Title        = title;
        Description  = description;
        Goal         = goal;
        TrainingType = trainingType;
    }

    public TrainingAssignment Assign(Trainer trainer)
    {
        Guard.Against.Default(trainer.Id, nameof(trainer));
        var assignment = new TrainingAssignment()
        {
            Trainer = trainer
        };
        _assignments.Add(assignment);
        return assignment;
    }

    public void ChangeTitle(string title)
    {
        _title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        _title = Guard.Against.MinLength(title, 5, nameof(title));
    }

    public void ChangeDescription(string description)
    {
        _description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        _description = Guard.Against.Between(description, 30, 500, nameof(description));
    }

    public void ChangeGoal(string goal)
    {
        _goal = Guard.Against.NullOrWhiteSpace(goal, nameof(goal));
        _goal = Guard.Against.Between(goal, 30, 500, nameof(goal));
    }

    public void ChangeType(TrainingType trainingType)
    {
        _trainingType = trainingType;
    }
}
