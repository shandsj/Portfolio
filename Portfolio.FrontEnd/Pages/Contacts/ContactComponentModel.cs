    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines a model for the <see cref="ContactComponent"/>.
    /// </summary>
    public class ContactComponentModel
    {
        /// <summary>
        /// Gets or sets the name of the contact.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the contact.
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required]
        [StringLength(2000, ErrorMessage = "Message is too long.")]
        public string? Message { get; set; }
    }