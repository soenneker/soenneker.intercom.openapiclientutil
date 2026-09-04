using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Intercom.HttpClients.Abstract;
using Soenneker.Intercom.OpenApiClientUtil.Abstract;
using Soenneker.Intercom.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Intercom.OpenApiClientUtil;

/// <inheritdoc cref="IIntercomOpenApiClientUtil" />
public sealed class IntercomOpenApiClientUtil : IIntercomOpenApiClientUtil
{
    private readonly AsyncSingleton<IntercomOpenApiClient> _client;

    public IntercomOpenApiClientUtil(IIntercomOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<IntercomOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new IntercomOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<IntercomOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
