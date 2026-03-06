using Aspire.Hosting;
using CommunityToolkit.Aspire.Testing;

namespace CommunityToolkit.Aspire.Hosting.Zig.Tests;

public class ZigAppPublicApiTes
{
    [Fact]
    public void AddRustAppShouldThrowWhenBuilderIsNull()
    {
        IDistributedApplicationBuilder builder = null!;
        const string name = "zig-app";
        const string workingDirectory = "zig_app";
        var action = () => builder.AddRustApp(name, workingDirectory);

        var exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal(nameof(builder), exception.ParamName);
    }

    [Fact]
    public void AddRustAppShouldThrowWhenNameIsNull()
    {
        IDistributedApplicationBuilder builder = new DistributedApplicationBuilder([]);
        const string name = null!;
        const string workingDirectory = "zig_app";

        var action = () => builder.AddRustApp(name!, workingDirectory);

        var exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal(nameof(name), exception.ParamName);
    }

    [Fact]
    public void AddRustAppShouldThrowWorkingDirectoryIsNull()
    {
        IDistributedApplicationBuilder builder = new DistributedApplicationBuilder([]);
        const string name = "zig-app";
        const string workingDirectory = null!;

        var action = () => builder.AddRustApp(name, workingDirectory!);

        var exception = Assert.Throws<ArgumentNullException>(action);
        Assert.Equal(nameof(workingDirectory), exception.ParamName);
    }
}
