namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Otp { get; set; }   // Remove in production - for development only
        public bool IsExistingUser { get; set; }
    }
}
