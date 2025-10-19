using Application.DTOs.Auth.RequestModel;
using Application.DTOs.Auth.ResponseModel;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Shared;
using Domain.Entities;
using Domains.Execptions;
using Microsoft.Extensions.Configuration;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWTManager _jwtManager;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork unitOfWork, IJWTManager jwtManager, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _jwtManager = jwtManager;
            _config = config;
        }

        public async Task<BaseResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _unitOfWork.Users.GetBySpecAsync(u => u.Email == request.EmailAddress);
            if (existingUser is not null)
                throw new ConflictException("User with this email already exists.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.EmailAddress,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var jwtRequest = new JwtTokenRequest
            {
                Id = user.Id.ToString(),
                FullName = user.FullName,
                Email = user.Email
            };

            var token = _jwtManager.CreateToken(
                _config["Jwt:Key"],
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                jwtRequest
            );

            return new BaseResponse<AuthResponse>
            {
                Success = true,
                Message = "Registration successful",
                Data = new AuthResponse
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Token = token
                }
            };
        }

        public async Task<BaseResponse<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _unitOfWork.Users.GetBySpecAsync(u => u.Email == request.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password.");

            var jwtRequest = new JwtTokenRequest
            {
                Id = user.Id.ToString(),
                FullName = user.FullName,
                Email = user.Email
            };

            var token = _jwtManager.CreateToken(
                _config["Jwt:Key"],
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                new JwtTokenRequest
                {
                    Id = user.Id.ToString(),
                    FullName = user.FullName,
                    Email = user.Email
                }

            );

            return new BaseResponse<AuthResponse>
            {
                Success = true,
                Message = "Login successful",
                Data = new AuthResponse
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Token = token
                }
            };

        }
    }
}
