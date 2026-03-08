using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class AdminLoginHandler : IRequestHandler<AdminLoginCommand, AdminLoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public AdminLoginHandler(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AdminLoginResponse> Handle(
            AdminLoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return new AdminLoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.Passwordhash
            );

            if (!isPasswordValid)
            {
                return new AdminLoginResponse
                {
                    Success = false,
                    Message = "Invalid email or password"
                };
            }

            // 🔥 Ensure user has Admin role
            var roleData = await _userRepository
                .GetUserRoleByNameAsync(user.Id, "Admin");

            if (roleData == null)
            {
                return new AdminLoginResponse
                {
                    Success = false,
                    Message = "User is not an Admin"
                };
            }

            var roles = new List<string> { roleData.Value.RoleName };

            var (accessToken, jti, expiry) =
                _jwtTokenService.GenerateToken(user.Id, roles);

            var refreshToken = new Refreshtoken
            {
                Id = Guid.NewGuid(),
                Userid = user.Id,
                Token = GenerateSecureRefreshToken(),
                Jwtid = jti,
                Createdat = DateTime.UtcNow,
                Expiresat = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new AdminLoginResponse
            {
                Success = true,
                Message = "Admin login successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                User = new AuthUserDto
                {
                    Id = user.Id,
                    FullName = user.Fullname,
                    Email = user.Email,
                    RoleId = roleData.Value.RoleId,
                    RoleName = roleData.Value.RoleName
                }
            };
        }

        private string GenerateSecureRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
