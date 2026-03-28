using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class DeliveryLoginHandler : IRequestHandler<DeliveryLoginCommand, DeliveryLoginResponse>
    {
        private const string DeliveryPartnerRoleName = "DeliveryPartner";

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public DeliveryLoginHandler(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<DeliveryLoginResponse> Handle(
            DeliveryLoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByPhoneAsync(request.Phone);

            if (user == null || !user.Isactive || user.Isdeleted)
            {
                return new DeliveryLoginResponse
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.Passwordhash);

            if (!isPasswordValid)
            {
                return new DeliveryLoginResponse
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            var roleData = await _userRepository
                .GetUserRoleByNameAsync(user.Id, DeliveryPartnerRoleName);

            if (roleData == null)
            {
                return new DeliveryLoginResponse
                {
                    Success = false,
                    Message = "Not a delivery partner"
                };
            }

            var roles = new List<string> { roleData.Value.RoleName };

            var (accessToken, jti, _) =
                _jwtTokenService.GenerateToken(user.Id, roles);

            // refreshtokens.* columns are timestamptz in PostgreSQL — Npgsql requires UTC DateTime.
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

            return new DeliveryLoginResponse
            {
                Success = true,
                Message = "Login successful",
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                User = new AuthUserDto
                {
                    Id = user.Id,
                    FullName = user.Fullname,
                    Phone = user.Phone ?? string.Empty,
                    Email = user.Email,
                    RoleId = roleData.Value.RoleId,
                    RoleName = roleData.Value.RoleName
                }
            };
        }

        private static string GenerateSecureRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
