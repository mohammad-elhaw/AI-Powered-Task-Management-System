namespace Notifications.Application.Email;

public interface IEmailSender
{
    Task SendEmail(string to, string subject, string htmlbody, 
        CancellationToken cancellationToken = default);
}
