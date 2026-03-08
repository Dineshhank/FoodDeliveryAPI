namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class VerifyOtpRequest
    {
        public string Phone { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }
}
