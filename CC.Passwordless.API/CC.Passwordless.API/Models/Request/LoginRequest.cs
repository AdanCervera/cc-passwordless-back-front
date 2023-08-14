using System.ComponentModel.DataAnnotations;

namespace CC.Passwordless.API.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
    }
}
