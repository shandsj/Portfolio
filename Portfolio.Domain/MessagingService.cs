using SendGrid;
using SendGrid.Helpers.Mail;

namespace Portfolio.Domain;

/// <summary>
/// Provides an implementation of the <see cref="IMessagingService" /> interface.
/// </summary>
public class MessagingService : IMessagingService
{
    /// <inheritdoc />
    public async Task SendMessageAsync(Message message, CancellationToken token)
    {
        var client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API_KEY", EnvironmentVariableTarget.Process));
        var email = MailHelper.CreateSingleEmail(
            new EmailAddress("jason.shands@gmail.com"),
            new EmailAddress("jason.shands@gmail.com"),
            $"Your portfolio received a message from {message.Name} ({message.Email})",
            message.Text,
            string.Empty);

        var response = await client.SendEmailAsync(email, token);
    }
}
