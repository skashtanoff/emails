using Emails.Extensions;
using Emails.Providers;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Emails;

/// <summary>
/// An SMTP server that could send an emails from a specific sender.
/// </summary>
public sealed class Server
{
    /// <summary>
    /// Sender instance used for sending email messages.
    /// <see cref="Sender"/>
    /// </summary>
    private readonly Sender _sender;
    
    /// <summary>
    /// SMTP server provider info.
    /// <see cref="ProviderInfo"/>
    /// </summary>
    private readonly ProviderInfo _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Server"/> class with the provided sender information.
    /// </summary>
    /// <param name="sender">The sender information for configuring the SMTP server.</param>
    /// <param name="provider">The API provider for a <paramref name="sender"/> email.</param>
    /// <see cref="ProviderInfo"/>
    public Server(Sender sender, ProviderInfo provider)
    {
        _sender = sender;
        _provider = provider;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Server"/> class with the Gmail provider as default.
    /// </summary>
    /// <param name="sender">The sender information for configuring the SMTP server.</param>
    /// <see cref="DefaultProviders.Gmail"/>
    public Server(Sender sender) : 
        this(sender, DefaultProviders.Gmail)
    {
    }
    
    /// <summary>
    /// Tries to sends an email message asynchronously.
    /// </summary>
    /// <param name="message">The email message to be sent.</param>
    /// <param name="token">CancellationToken for an operation.</param>
    /// <returns><c>true</c> if the sending succeeded, <c>false</c> otherwise.</returns>
    /// <remarks>Swallows all inner exceptions (maybe will be changed in future)</remarks>
    public async Task<bool> TrySendAsync(Message message, CancellationToken token = default)
    {
        using var envelope = await BuildEnvelopeAsync(message);
        using var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(_provider.Url, _provider.Port, SecureSocketOptions.Auto, token);
            await client.AuthenticateAsync(_sender.Email, _sender.Password, token);
            await client.SendAsync(envelope, token);
            await client.DisconnectAsync(true, token);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    /// <summary>
    /// Creates a <see cref="MailKit"/> compatible <see cref="MimeMessage"/> from the <param name="message"/>.
    /// </summary>
    /// <param name="message">The message to be sent.</param>
    /// <returns><see cref="MimeMessage"/> created from the <paramref name="message"/></returns>
    private async Task<MimeMessage> BuildEnvelopeAsync(Message message)
    {
        var envelope = new MimeMessage();
        envelope.From.Add(new MailboxAddress(_sender.DisplayName, _sender.Email));
        envelope.To.FillFrom(message.To);
        envelope.Cc.FillFrom(message.Cc);
        envelope.Bcc.FillFrom(message.Bcc);
        envelope.Subject = message.Subject;
        envelope.Body = await BuildBodyAsync(message);
        return envelope;
    }
    
    /// <summary>
    /// Creates a <see cref="MimeMessage"/> body as <see cref="MimeEntity"/> from the <paramref name="message"/>.
    /// <list type="bullet">
    /// <item><description>If <see cref="Message.IsHtml"/> is <c>false</c>, creates a plain text body.</description></item>
    /// <item><description>If <see cref="Message.IsHtml"/> is <c>true</c>, creates an HTML body.</description></item>
    /// <item><description>If <see cref="Message.Attachments"/> is not empty, adds them all to an evelope. </description></item>
    /// </list>
    /// </summary>
    /// <param name="message">The message to be sent.</param>
    /// <returns><see cref="MimeEntity"/> created from the <paramref name="message"/></returns>
    private static async Task<MimeEntity> BuildBodyAsync(Message message)
    {
        var builder = new BodyBuilder();

        if (message.IsHtml)
        {
            builder.HtmlBody = message.Body;
        }
        else
        {
            builder.TextBody = message.Body;
        }
        
        var attachments = builder.Attachments;
        foreach (var file in message.Attachments)
        {
            await attachments.AddAsync(file.Name, file.Stream);
        }
        return builder.ToMessageBody();
    }
}