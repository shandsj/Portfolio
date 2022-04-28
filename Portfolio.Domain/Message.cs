namespace Portfolio.Domain;

/// <summary>
/// A value object that represents a message.
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the name of the sender.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email address of the sender.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the text.
    /// </summary>
    public string Text { get; set; }
}