using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using System.Threading;

namespace Soenneker.Upstash.OpenApiClient.Tests;

public sealed class UpstashOpenApiClientTests
{
    [Test]
    public async ValueTask List_databases_builds_the_request_and_deserializes_the_response(CancellationToken cancellationToken)
    {
        using var handler = new UpstashResponseHandler();
        using var httpClient = new HttpClient(handler);
        using var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);
        var client = new UpstashOpenApiClient(adapter);
        var databases = await client.Redis.Databases.GetAsync(cancellationToken: cancellationToken);
        await Assert.That(handler.RequestedUrl).IsEqualTo("https://api.upstash.com/v2/redis/databases");
        await Assert.That(databases!.Count).IsEqualTo(1);
        await Assert.That(databases[0].DatabaseId).IsEqualTo("db-123");
        await Assert.That(databases[0].DatabaseName).IsEqualTo("test");
        await Assert.That(databases[0].Port).IsEqualTo(6379);
    }
}
