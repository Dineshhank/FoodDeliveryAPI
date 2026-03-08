using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using MediatR;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOtpRepository _otpRepository;

        public LoginHandler(
            IUserRepository userRepository,
            IUserOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            // Check if user exists
            var user = await _userRepository.GetByPhoneAsync(request.Phone);

            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();

            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var userOtp = new Userotp
            {
                Id = Guid.NewGuid(),
                Phone = request.Phone,
                Otpcode = otp,
                Isused = false,
                Attemptcount = 0,
                Expiresat = now.AddMinutes(5),
                Createdat = now
            };

            await _otpRepository.AddOtpAsync(userOtp);
            await _otpRepository.SaveChangesAsync();

            return new LoginResponse
            {
                Success = true,
                Message = "OTP sent successfully",
                Otp = otp, // Remove in production
                IsExistingUser = user != null
            };
        }
    }
}
