using Catalog.Shared.HttpClients.Catalog;

namespace Catalog.Web.WebAssembly.Extensions;

public static class ApiErrorExtensions
{
    public static bool HasResult<T>(this ApiException apiException)
    {
        if (apiException is ApiException<T>)
        {
            return true;
        }

        return false;
    }

    public static bool HashErrorResponse(this ApiException apiException)
    {
        return apiException.HasResult<ErrorResponse>();
    }

    public static ErrorResponse? ErrorResponse(this ApiException apiException)
    {
        return (apiException as ApiException<ErrorResponse>)?.Result;
    }
}
