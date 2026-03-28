namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class DeliveryLoginRequest
    {
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
