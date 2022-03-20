namespace Catalog.Api.Endpoints;

public class ErrorResponse
{
    public ICollection<Error> Errors { get; set; } = new List<Error>();
}

public class Error
{
    public string? ErrorCode { get; set; }

    public string? ErrorMessage { get; set; }
}