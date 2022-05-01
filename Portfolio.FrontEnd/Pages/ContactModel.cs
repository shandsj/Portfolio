    using System.ComponentModel.DataAnnotations;

    public class ContactModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long.")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(2000, ErrorMessage = "Message is too long.")]
        public string? Message { get; set; }
    }