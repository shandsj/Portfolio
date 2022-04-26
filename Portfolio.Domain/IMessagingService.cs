namespace Portfolio.Domain;

/// <summary>
/// Defines an interface for a service used to send messages.
/// </summary>
public interface IMessagingService
{
    /// <summary>
    /// Asynchronously sends the specified message.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>The task.</returns>
    Task SendMessageAsync(Message message, CancellationToken token);
}
