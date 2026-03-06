using Aspire.Hosting;
using CommunityToolkit.Aspire.Utils;
using CommunityToolkit.Aspire.Testing;

namespace CommunityToolkit.Aspire.Hosting.Zig.Tests;

public class AddZigAppTest
{
    // TODO change to zig implementation
    [Fact]
    public async Task AddRustAppAddsAnnotationMetadata()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        var workingDirectory = "../../examples/rust/actix_api";
        var rustApp = appBuilder.AddRustApp("zig-app", workingDirectory);

        using var app = appBuilder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var resource = Assert.Single(appModel.Resources.OfType<ZigAppExecutableResource>());
        workingDirectory = Path.Combine(appBuilder.AppHostDirectory, workingDirectory).NormalizePathForCurrentPlatform();
        Assert.Equal("zig-app", resource.Name);
        Assert.Equal(workingDirectory, resource.WorkingDirectory);
        Assert.Equal("zig", resource.Command);
        var args = await resource.GetArgumentListAsync();
        Assert.Collection(args,
            arg => Assert.Equal("run", arg));
    }

    [Fact]
    public async Task AddRustAppWithArgsAddsAnnotationMetadata()
    {
        var appBuilder = DistributedApplication.CreateBuilder();

        var workingDirectory = "../../examples/rust/actix_api";
        var rustApp = appBuilder.AddRustApp("zig-app", workingDirectory, ["--verbose"]);

        using var app = appBuilder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var resource = Assert.Single(appModel.Resources.OfType<ZigAppExecutableResource>());
        workingDirectory = Path.Combine(appBuilder.AppHostDirectory, workingDirectory).NormalizePathForCurrentPlatform();
        Assert.Equal("zig-app", resource.Name);
        Assert.Equal(workingDirectory, resource.WorkingDirectory);
        Assert.Equal("zig", resource.Command);
        var args = await resource.GetArgumentListAsync();
        Assert.Collection(args,
            arg => Assert.Equal("run", arg),
            arg => Assert.Equal("--verbose", arg));
    }
}
