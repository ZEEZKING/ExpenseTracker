using Application.DTOs.Auth.RequestModel;

namespace Application.Interfaces
{
    public interface IJWTManager
    {
        string CreateToken(string key, string issuer, string audience, JwtTokenRequest model);
    }
}
