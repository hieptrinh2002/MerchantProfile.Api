using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MerchantProfile.Api.Models.Dtos.Request
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Description length can't be more than 500.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Invalid URL")]
        public string? LinkWebsite { get; set; }

        [StringLength(200, ErrorMessage = "Address length can't be more than 200.")]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }
        public string Username { get; set; }
    }
}
