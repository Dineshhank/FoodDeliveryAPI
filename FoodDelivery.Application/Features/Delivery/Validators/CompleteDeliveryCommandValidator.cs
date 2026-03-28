using FoodDelivery.Application.Features.Delivery.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Delivery.Validators
{
    public class CompleteDeliveryCommandValidator : AbstractValidator<CompleteDeliveryCommand>
    {
        public CompleteDeliveryCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
