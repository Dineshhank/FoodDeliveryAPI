using FoodDelivery.Application.Features.Delivery.Commands;
using FluentValidation;

namespace FoodDelivery.Application.Features.Delivery.Validators
{
    public class StartDeliveryToCustomerCommandValidator : AbstractValidator<StartDeliveryToCustomerCommand>
    {
        public StartDeliveryToCustomerCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
