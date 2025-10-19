namespace Application.DTOs.Auth.RequestModel
{
    public class JwtTokenRequest
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
