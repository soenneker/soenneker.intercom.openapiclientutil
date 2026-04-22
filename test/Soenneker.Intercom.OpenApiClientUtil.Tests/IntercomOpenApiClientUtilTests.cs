using Soenneker.Intercom.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Intercom.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class IntercomOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IIntercomOpenApiClientUtil _openapiclientutil;

    public IntercomOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IIntercomOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
