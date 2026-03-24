using Soenneker.Intercom.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Intercom.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IIntercomOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<IntercomOpenApiClient> Get(CancellationToken cancellationToken = default);
}
