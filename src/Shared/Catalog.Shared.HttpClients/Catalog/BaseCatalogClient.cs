using System.Text;

namespace Catalog.Shared.HttpClients.Catalog;

public class BaseCatalogClient
{
    protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder)
    {
        return Task.CompletedTask;
    }

    protected Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string urlBuilder)
    {
        return Task.CompletedTask;
    }

    protected Task ProcessResponseAsync(HttpClient client, HttpResponseMessage responseMessage, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
