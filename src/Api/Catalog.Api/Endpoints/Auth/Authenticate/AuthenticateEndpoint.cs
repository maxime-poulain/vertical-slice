using Ardalis.ApiEndpoints;
using Catalog.Api.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints.Auth.Authenticate;

public class AuthenticateEndpoint : EndpointBaseAsync
    .WithRequest<AuthenticateRequest>
    .WithActionResult<AuthenticationResponse>
{
    public override Task<ActionResult<AuthenticationResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }
}
