using FoodDelivery.Application.Features.Auth.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Auth.Validators
{
    public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpCommandValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required")
                .Length(6).WithMessage("OTP must be 6 digits")
                .Matches(@"^[0-9]+$").WithMessage("OTP must contain only digits");
        }
    }
}
