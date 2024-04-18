namespace Emails.Providers;

/// <summary>
/// Set of an available default SMTP server providers.
/// <see cref="Yandex"/> for Yandex-based SMTP
/// <see cref="Gmail"/> for Google ones (including custom workspaces)
/// </summary>
public static class DefaultProviders
{
    /// <summary>
    /// SMTP provider info for Gmail servers
    /// </summary>
    public static readonly ProviderInfo Gmail = new()
    {
        Url = "smtp.gmail.com",
        Port = 587
    };

    /// <summary>
    /// SMTP provider info for Yandex servers
    /// </summary>
    public static readonly ProviderInfo Yandex = new()
    {
        Url = "smtp.yandex.ru",
        Port = 465
    };
}