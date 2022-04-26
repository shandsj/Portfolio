namespace Portfolio.Domain;

/// <summary>
/// A value object that represents a message.
/// </summary>
public class Message
{
    public Message(string name, string email, string subject, string text)
    {
        Name = name;
        Email = email;
        Subject = subject;
        Text = text;
    }

    /// <summary>
    /// Gets or sets the name of the sender.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the email address of the sender.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Gets or sets the subject.
    /// </summary>
    public string Subject { get; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string Text { get; }
}