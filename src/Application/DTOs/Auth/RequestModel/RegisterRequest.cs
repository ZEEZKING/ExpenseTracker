using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth.RequestModel
{
    public class RegisterRequest
    {
        [Required]
        public string FullName { get; set; } = default!;
        [EmailAddress]
        public string EmailAddress { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
