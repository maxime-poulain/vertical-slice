using Ardalis.GuardClauses;
using Catalog.Api.Domain.Entities.Base;
using Catalog.Api.Domain.Entities.TrainerAggregate;
using Catalog.Api.Domain.Extensions;
using Catalog.Shared.Enumerations.Training;

namespace Catalog.Api.Domain.Entities.TrainingAggregate;

public class Training : Entity, IEntity
{
    private string _title = null!;

    public string Title
    {
        get  => _title;
        init => ChangeTitle(value);
    }

    private string _description = null!;

    public string Description
    {
        get  => _description;
        init => ChangeDescription(value);
    }

    private string _goal = null!;

    public string Goal
    {
        get  => _goal;
        init => ChangeGoal(value);
    }

    private readonly HashSet<TrainingAssignment> _assignments;
    private readonly HashSet<TrainingAudience> _audiences;
    private readonly HashSet<TrainingTopic> _topics;
    private readonly HashSet<TrainingAttendance> _attendances;
    private readonly HashSet<TrainingVatJustification> _vatJustifiations;

    public IReadOnlySet<TrainingAudience> Audiences => _audiences;

    public IReadOnlySet<TrainingAttendance> Attendances => _attendances;

    public IReadOnlySet<TrainingAssignment> Assignments => _assignments;

    public IReadOnlySet<TrainingTopic> Topics => _topics;

    public IReadOnlySet<TrainingVatJustification> VatJustifications => _vatJustifiations;

    private Training()
    {
        _assignments      = new HashSet<TrainingAssignment>();
        _topics           = new HashSet<TrainingTopic>();
        _attendances      = new HashSet<TrainingAttendance>();
        _vatJustifiations = new HashSet<TrainingVatJustification>();
        _audiences        = new HashSet<TrainingAudience>();
    }

    public Training(string title, string description, string goal) : this()
    {
        Title        = title;
        Description  = description;
        Goal         = goal;
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

    public void SetAttendance(IEnumerable<int>? attendances)
    {
        SetAttendance(attendances?.Select(Attendance.FromValue));
    }

    public void SetAttendance(IEnumerable<Attendance>? attendances)
    {
        SetRelational(attendances, attendance => new TrainingAttendance(this, attendance), _attendances);
    }

    public void SetAudience(List<int>? requestAudiences)
    {
        SetAudience(requestAudiences?.Select(Audience.FromValue));
    }

    public void SetAudience(IEnumerable<Audience>? audiences)
    {
        SetRelational(audiences, audience => new TrainingAudience(this, audience), _audiences);
    }

    public void SetTopics(IEnumerable<int>? topics)
    {
        SetTopics(topics?.Select(Topic.FromValue));
    }

    public void SetTopics(IEnumerable<Topic>? topics)
    {
        SetRelational(topics, topic => new TrainingTopic(this, topic), _topics);
    }

    public void SetVatJustifications(IEnumerable<int>? vatJustificationIds)
    {
        SetVatJustifications(vatJustificationIds?.Select(VatJustification.FromValue));
    }

    public void SetVatJustifications(IEnumerable<VatJustification>? vatJustifications)
    {
        SetRelational(vatJustifications, vat => new TrainingVatJustification(this, vat), _vatJustifiations);
    }
}
