using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Upstash.OpenApiClient.Tests;

internal sealed class UpstashResponseHandler : HttpMessageHandler
{
    public string? RequestedUrl { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        RequestedUrl = request.RequestUri!.AbsoluteUri;
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("""[{"database_id":"db-123","database_name":"test","region":"us-east-1","port":6379,"tls":true}]""",
                System.Text.Encoding.UTF8, "application/json")
        });
    }
}
