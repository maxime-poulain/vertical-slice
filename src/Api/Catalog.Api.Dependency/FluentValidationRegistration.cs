﻿using Catalog.Api.Application.Features.Training.Create.Command;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api.Dependency;

public static class FluentValidationRegistration
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services.AddFluentValidation(fluentValidationMvcConfiguration =>
        {
            fluentValidationMvcConfiguration.RegisterValidatorsFromAssembly(typeof(CreateTrainingCommandValidator).Assembly);
        });
    }
}