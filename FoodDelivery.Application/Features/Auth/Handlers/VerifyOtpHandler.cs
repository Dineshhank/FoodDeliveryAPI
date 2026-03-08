using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;    
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOtpRepository _otpRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public VerifyOtpHandler(
    IUserRepository userRepository,
    IUserOtpRepository otpRepository,
    IJwtTokenService jwtTokenService,
    IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _jwtTokenService = jwtTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<VerifyOtpResult> Handle(
            VerifyOtpCommand request,
            CancellationToken cancellationToken)
        {
            var userOtp = await _otpRepository.GetValidOtpAsync(request.Phone, request.Otp);
            if (userOtp == null)
            {
                return new VerifyOtpResult
                {
                    Success = false,
                    Message = "Invalid or expired OTP"
                };
            }

            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var user = await _userRepository.GetByPhoneAsync(request.Phone);
            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    Fullname = string.Empty,
                    Phone = request.Phone,
                    Email = null,
                    Passwordhash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()),
                   
                    Isactive = true,
                    Isphoneverified = true,
                    Isemailverified = false,
                    Isdeleted = false,
                    Createdat = now
                };
                await _userRepository.AddUserAsync(user);
                await _userRepository.AssignRoleAsync(user.Id, "Customer");
            }
            else
            {
                user.Lastloginat = now;
                user.Isphoneverified = true;
                await _userRepository.UpdateUserAsync(user);
            }

            await _otpRepository.MarkOtpAsUsedAsync(userOtp);
            await _userRepository.SaveChangesAsync();

            var roleData = await _userRepository
    .GetUserRoleByNameAsync(user.Id, "Customer");
            var roles = new List<string> { roleData.Value.RoleName };

            var (accessToken, jti, expiry) =
                _jwtTokenService.GenerateToken(user.Id, roles);

            var refreshToken = new Refreshtoken
            {
                Id = Guid.NewGuid(),
                Userid = user.Id,
                Token = GenerateSecureRefreshToken(),
                Jwtid = jti,
                Isrevoked = false,
                Isused = false,
                Createdat = DateTime.UtcNow,
                Expiresat = DateTime.UtcNow.AddDays(7),
               
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return new VerifyOtpResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Message = "Login successful",
                User = new AuthUserDto
                {
                    Id = user.Id,
                    FullName = user.Fullname,
                    Phone = user.Phone,
                    Email = user.Email,
                    RoleId = roleData.Value.RoleId,
                    RoleName = roleData.Value.RoleName
                },
                
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
