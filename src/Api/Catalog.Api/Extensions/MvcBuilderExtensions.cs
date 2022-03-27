using Catalog.Api.Endpoints;
using Catalog.Api.Features;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder ConfigureApiBehaviorOptions(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseDelegate;
        });
    }

    /// <inheritdoc cref="Microsoft.AspNetCore.Mvc.ApiBehaviorOptions.InvalidModelStateResponseFactory"/>
    private static IActionResult InvalidModelStateResponseDelegate(ActionContext actionContext)
    {
        var validationResultFeature = actionContext.HttpContext.Features.Get<IValidationResultFeature>();
        if (validationResultFeature is not null)
        {
            return new BadRequestObjectResult(validationResultFeature.ValidationResult.ToErrorResponse());
        }

        return new BadRequestObjectResult(actionContext.ModelState.ToErrorResponse());
    }
}