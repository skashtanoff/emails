namespace Emails;

/// <summary>
/// An information about an email sender.
/// </summary>
public sealed class Sender
{
    /// <summary>
    /// The sender email address.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// The sender password (app password in general).
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Sender display name. Default is "noreply."
    /// </summary>
    public string DisplayName { get; init; } = "noreply";
}