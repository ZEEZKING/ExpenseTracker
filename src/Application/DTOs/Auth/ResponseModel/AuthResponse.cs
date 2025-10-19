namespace Application.DTOs.Auth.ResponseModel
{
    public record AuthResponse
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
