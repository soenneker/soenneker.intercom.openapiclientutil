using Soenneker.Intercom.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Intercom.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class IntercomOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IIntercomOpenApiClientUtil _openapiclientutil;

    public IntercomOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IIntercomOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
