namespace Catalog.Api.Application.Services.Authentication;

public class AuthenticationResponse
{
    public static readonly AuthenticationResponse InvalidUserOrNotFound = new();

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }
}
