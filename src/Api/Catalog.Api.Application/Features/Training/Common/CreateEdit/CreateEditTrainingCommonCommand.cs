using Catalog.Api.Domain.CQS;
using Catalog.Api.Domain.Entities.TrainingAggregate.Message;

namespace Catalog.Api.Application.Features.Training.Common.CreateEdit;

public abstract class CreateEditTrainingCommonCommand<TResponse> : TrainingEditMessage, ICommand<TResponse>
{
}
