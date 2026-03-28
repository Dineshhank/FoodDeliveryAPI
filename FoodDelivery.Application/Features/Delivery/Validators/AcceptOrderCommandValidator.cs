using FoodDelivery.Application.Features.Delivery.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Delivery.Validators
{
    public class AcceptOrderCommandValidator : AbstractValidator<AcceptOrderCommand>
    {
        public AcceptOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
