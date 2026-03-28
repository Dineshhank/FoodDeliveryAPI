using FoodDelivery.Application.Features.Delivery.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Delivery.Validators
{
    public class ReachedRestaurantCommandValidator : AbstractValidator<ReachedRestaurantCommand>
    {
        public ReachedRestaurantCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
