using FoodDelivery.Application.Features.Auth.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Auth.Validators
{
    public class DeliveryLoginCommandValidator : AbstractValidator<DeliveryLoginCommand>
    {
        public DeliveryLoginCommandValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
