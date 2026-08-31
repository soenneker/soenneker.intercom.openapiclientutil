[![](https://img.shields.io/nuget/v/soenneker.intercom.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.intercom.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.intercom.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.intercom.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.intercom.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.intercom.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.intercom.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.intercom.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Intercom.OpenApiClientUtil

Create and reuse an authenticated Intercom generated client over the shared Intercom HTTP transport.

## Install

```bash
dotnet add package Soenneker.Intercom.OpenApiClientUtil
```

## Configure

```json
{
  "Intercom": {
    "ApiKey": "<access token>"
  }
}
```

The underlying HTTP provider defaults to `https://api.intercom.io` and `Authorization: Bearer <access token>`. It also supports `Intercom:ClientBaseUrl`, `Intercom:AuthHeaderName`, and `Intercom:AuthHeaderValueTemplate`; use `{token}` in the value template.

## Register

```csharp
using Soenneker.Intercom.OpenApiClientUtil.Registrars;

services.AddIntercomOpenApiClientUtilAsScoped();
```

The scoped utility deliberately keeps `IIntercomOpenApiHttpClient` singleton. Disposing a scope releases that utility's generated-client wrapper without tearing down the long-lived HTTP client used by later scopes. Use `AddIntercomOpenApiClientUtilAsSingleton()` when the generated client should also live for the application lifetime.

## Usage

```csharp
using Soenneker.Intercom.OpenApiClient;
using Soenneker.Intercom.OpenApiClient.Models;
using Soenneker.Intercom.OpenApiClientUtil.Abstract;

IntercomOpenApiClient client = await clientUtil.Get(cancellationToken);

AdminList? admins = await client.Admins.GetAsync(
    cancellationToken: cancellationToken);
```

Concurrent and repeated `Get()` calls on the same utility reuse its lazily created generated client. Cancellation affects first-time initialization; pass the token separately to generated request methods as shown above.

Authentication is supplied by the underlying HTTP provider, so the Kiota adapter does not add a second or conflicting header. Let the service container dispose the utility and provider.
