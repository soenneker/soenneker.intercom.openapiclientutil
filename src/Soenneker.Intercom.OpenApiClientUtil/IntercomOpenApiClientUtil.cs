using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Intercom.HttpClients.Abstract;
using Soenneker.Intercom.OpenApiClientUtil.Abstract;
using Soenneker.Intercom.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Intercom.OpenApiClientUtil;

///<inheritdoc cref="IIntercomOpenApiClientUtil"/>
public sealed class IntercomOpenApiClientUtil : IIntercomOpenApiClientUtil
{
    private readonly AsyncSingleton<IntercomOpenApiClient> _client;

    public IntercomOpenApiClientUtil(IIntercomOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<IntercomOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Intercom:ApiKey");
            string authHeaderValueTemplate = configuration["Intercom:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
