namespace Emails;

/// <summary>
/// Represents a unified abstracted representation of the email message.
/// Can be used to send plain text or HTML messages with optional attachments.
/// <see cref="Attachment"/>
/// </summary>
public sealed class Message
{
    /// <summary>
    /// The recipients of the email message.
    /// </summary>
    public required IEnumerable<string> To { get; init; } = Enumerable.Empty<string>();
    
    /// <summary>
    /// Tthe recipients of the email carbon copy (CC).
    /// </summary>
    public IEnumerable<string> Cc { get; init; } = Enumerable.Empty<string>();

    /// <summary>
    /// The recipients of the email blind carbon copy (BCC).
    /// </summary>
    public IEnumerable<string> Bcc { get; init; } = Enumerable.Empty<string>();

    /// <summary>
    /// The subject of the email message.
    /// </summary>
    public string Subject { get; init; } = "noreply";

    /// <summary>
    /// The body of the email message.
    /// </summary>
    public required string Body { get; init; }
    
    /// <summary>
    /// Flag indicating either the <see cref="Body"/> should be interpreted as HTML.
    /// </summary>
    public bool IsHtml { get; init; } = false;
    
    /// <summary>
    /// The attachments pinned to the email message.
    /// </summary>
    public IEnumerable<Attachment> Attachments { get; init; } = Enumerable.Empty<Attachment>();
}