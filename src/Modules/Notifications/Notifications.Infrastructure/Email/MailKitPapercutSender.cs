using MailKit.Net.Smtp;
using MimeKit;
using Notifications.Application.Email;

namespace Notifications.Infrastructure.Email;

public class MailKitPapercutSender : IEmailSender
{
    public async Task SendEmail(string to, string subject, string htmlbody, CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Tasking System", "no-reply@tasking.local"));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlbody };

        using var client = new SmtpClient();
        await client.ConnectAsync("localhost", 25, false, cancellationToken);
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}
