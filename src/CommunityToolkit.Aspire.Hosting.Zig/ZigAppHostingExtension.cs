
using Aspire.Hosting.ApplicationModel;
using CommunityToolkit.Aspire.Utils;
namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for adding Zig applications to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class ZigAppHostingExtension
{
    /// <summary>
    /// Adds a Zig application to the application model. Executes the executable Zig app.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/> to add the resource to.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="workingDirectory">The working directory to use for the command.</param>
    /// <param name="args">The optional arguments to be passed to the executable when it is started.</param>
    /// <returns>A reference to the <see cref="IResourceBuilder{T}"/>.</returns>

    public static IResourceBuilder<ZigAppExecutableResource> AddZigApp(this IDistributedApplicationBuilder builder, [ResourceName] string name, string workingDirectory, string[]? args = null)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(workingDirectory, nameof(workingDirectory));

        string[] allArgs = args is { Length: > 0 }
            ? ["run", .. args]
            : ["run"];

        workingDirectory = Path.Combine(builder.AppHostDirectory, workingDirectory).NormalizePathForCurrentPlatform();

        var resource = new ZigAppExecutableResource(name, workingDirectory);
        return builder.AddResource(resource)
            .WithZigDefaults()
            .WithArgs(allArgs)
            .PublishAsDockerFile();
    }

    private static IResourceBuilder<ZigAppExecutableResource> WithZigDefaults(
        this IResourceBuilder<ZigAppExecutableResource> builder) => builder.WithOtlpExporter();
}
