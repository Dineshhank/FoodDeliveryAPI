namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class DeliveryLoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public AuthUserDto? User { get; set; }
    }
}
