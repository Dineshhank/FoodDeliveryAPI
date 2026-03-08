using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public RefreshTokenHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RefreshTokenResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (storedToken == null ||
                storedToken.Isrevoked ||
                storedToken.Isused ||
                storedToken.Expiresat < DateTime.UtcNow)
            {
                throw new Exception("Invalid refresh token");
            }

            storedToken.Isused = true;

            var user = await _userRepository.GetByIdAsync(storedToken.Userid);

            var roles = await _userRepository.GetUserRolesAsync(user.Id);

            var (accessToken, jti, expiry) =
                _jwtTokenService.GenerateToken(user.Id, roles);

            var newRefreshToken = new Domain.Entities.Refreshtoken
            {
                Id = Guid.NewGuid(),
                Userid = user.Id,
                Token = GenerateSecureRefreshToken(),
                Jwtid = jti,
                Createdat = DateTime.UtcNow,
                Expiresat = DateTime.UtcNow.AddDays(7),
                Isused = false,
                Isrevoked = false
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token
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
