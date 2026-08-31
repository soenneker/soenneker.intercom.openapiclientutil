using Soenneker.Intercom.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Intercom.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a lazily created Intercom generated client over the shared authenticated transport.
/// </summary>
public interface IIntercomOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the Intercom generated client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the configured client.</returns>
    ValueTask<IntercomOpenApiClient> Get(CancellationToken cancellationToken = default);
}
