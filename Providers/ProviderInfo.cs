namespace Emails.Providers;

/// <summary>
/// An information about the SMTP server, required to establish a connection to it.
/// </summary>
public sealed class ProviderInfo
{
    /// <summary>
    /// The URL of an SMTP server.
    /// For example, <see cref="DefaultProviders"/> 
    /// </summary>
    public required string Url { get; init; }
        
    /// <summary>
    /// The port of an SMTP server.
    /// For example, <see cref="DefaultProviders"/> 
    /// </summary>
    /// <remarks>Must be between <c>0</c> and <c>65535</c></remarks>
    public int Port { get; init; } 
}