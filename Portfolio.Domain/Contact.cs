namespace Portfolio.Domain;

/// <summary>
/// A value object that represents a contact.
/// </summary>
public class Contact
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
    /// Gets or sets the message.
    /// </summary>
    public string Message { get; set; }
}