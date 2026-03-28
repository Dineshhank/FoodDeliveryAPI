using FoodDelivery.Application.Features.Auth.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Auth.Validators
{
    public class RegisterDeliveryPartnerCommandValidator : AbstractValidator<RegisterDeliveryPartnerCommand>
    {
        public RegisterDeliveryPartnerCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}
