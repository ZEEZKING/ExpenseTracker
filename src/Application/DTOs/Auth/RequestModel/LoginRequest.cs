using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth.RequestModel
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
