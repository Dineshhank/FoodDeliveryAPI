namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class RegisterDeliveryPartnerResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
    }
}
