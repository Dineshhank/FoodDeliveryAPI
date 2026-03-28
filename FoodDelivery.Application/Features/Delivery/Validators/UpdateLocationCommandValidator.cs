using FoodDelivery.Application.Features.Delivery.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Delivery.Validators
{
    public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90m, 90m)
                .WithMessage("Latitude must be between -90 and 90");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180m, 180m)
                .WithMessage("Longitude must be between -180 and 180");
        }
    }
}
