using CommunityToolkit.Aspire.Testing;

namespace CommunityToolkit.Aspire.Hosting.Zig.Tests;

public class AppHostTest(AspireIntegrationTestFixture<Projects.CommunityToolkit_Aspire_Hosting_Zig_AppHost> fixture) : IClassFixture<AspireIntegrationTestFixture<Projects.CommunityToolkit_Aspire_Hosting_Zig_AppHost>>
{
    [Fact]
    public async Task ResourceStartsAndRespondsOk()
    {
        var appName = "zig-app";

        var rns = fixture.App.Services.GetRequiredService<ResourceNotificationService>();
        _ = await rns.WaitForResourceHealthyAsync(appName).WaitAsync(TimeSpan.FromMinutes(5));

        var httpClient = fixture.CreateHttpClient(appName);
        var response = await httpClient.GetAsync("/ping");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
