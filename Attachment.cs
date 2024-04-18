using MimeKit;

namespace Emails;

/// <summary>
/// An adapter class that converts a file binary representation to 
/// a <see cref="MimeEntity"/> format, which is acceptable by underlying SMTP client
/// </summary>
public sealed class Attachment
{
    /// <summary>
    /// Stream containing the attachment data.
    /// </summary>
    public required Stream Stream { get; init; }
    
    /// <summary>
    /// Name of the attachment.
    /// </summary>
    public required string Name { get; init; }
}