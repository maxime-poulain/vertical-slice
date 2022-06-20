namespace Catalog.Api.Application.Services.Authentication;

public class AuthenticateRequest
{
    public string? Email { get; set; }

    public string? Password { get; set; }
}