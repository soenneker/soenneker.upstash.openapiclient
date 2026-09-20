# Soenneker.Upstash.OpenApiClient

A generated .NET 10 Kiota client for the [Upstash Developer API](https://upstash.com/docs/devops/developer-api/introduction).

The client covers the management operations in the [official specification](https://raw.githubusercontent.com/upstash/docs/main/devops/developer-api/openapi.yaml), including Redis databases, Vector indexes, Search, QStash account resources, teams, and audit logs. It does not replace a Redis data client or the separate QStash messaging API.

For configured authentication, dependency injection, and cached instances, use `Soenneker.Upstash.OpenApiClientUtil`. To manage your own HTTP client:

```csharp
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Upstash.OpenApiClient;

using var http = new HttpClient();
http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
    "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{email}:{apiKey}")));
using var adapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: http);
var client = new UpstashOpenApiClient(adapter);
var databases = await client.Redis.Databases.GetAsync(cancellationToken: cancellationToken);
```

Use a native Upstash account's Developer API credentials. Obtain them from secret configuration; do not embed them in code.

Generated files under `src/Soenneker.Upstash.OpenApiClient` should be changed through the companion `Soenneker.Upstash.Runners.OpenApiClient` runner. `openapi.fixed.json` records the normalized specification used to generate the client; `kiota-lock.json` records the generation settings.
