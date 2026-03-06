namespace Aspire.Hosting.ApplicationModel;

/// <summary>
/// A resource that represents a Zig appplication
/// </summary>
/// <param name="name">the name of the resoucre</param>
/// <param name="workingDirecotry">The working direcotry to use for the command</param>

public class ZigAppExecutableResource(string name, string workingDirecotry)
    : ExecutableResource(name, "zig", workingDirecotry), IResourceWithServiceDiscovery
{
}
