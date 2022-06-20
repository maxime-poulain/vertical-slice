using Catalog.Api.Application.Features.Trainings.Create.Command;
using Catalog.Api.Application.MediatR.Behaviors;
using Catalog.Api.Application.MediatR.Behaviors.Logging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Dependency;

public static class MediatRRegistration
{
    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(CreateTrainingCommand))
            .AddPipelineBehaviors();
    }

    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        // The order of pipelines is FIFO.
        return services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryLoggingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandLoggingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(NoEfTrackingDuringQueryPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    }
}
