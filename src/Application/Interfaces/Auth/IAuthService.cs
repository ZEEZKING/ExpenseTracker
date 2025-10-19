using Application.DTOs.Auth.RequestModel;
using Application.DTOs.Auth.ResponseModel;
using Application.Shared;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<BaseResponse<AuthResponse>> RegisterAsync(RegisterRequest request);
        Task<BaseResponse<AuthResponse>> LoginAsync(LoginRequest request);
    }
}
