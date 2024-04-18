using MimeKit;

namespace Emails.Extensions;

internal static class InternetAddressListExtensions
{
    /// <summary>
    /// Adds an emails from <paramref name="emails"/> to the <paramref name="addresses"/> collection.
    /// </summary>
    /// <param name="addresses"><see cref="InternetAddressList"/> to be filled.</param>
    /// <param name="emails">List of emails to be added.</param>
    public static void FillFrom(this InternetAddressList addresses, IEnumerable<string> emails)
    {
        foreach (var email in emails)
        {
            var address = new MailboxAddress("", email);
            addresses.Add(address);
        }        
    }
}