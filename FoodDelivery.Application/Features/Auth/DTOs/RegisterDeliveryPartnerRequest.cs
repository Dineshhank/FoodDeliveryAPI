namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class RegisterDeliveryPartnerRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? VehicleType { get; set; }
        public string? VehicleNumber { get; set; }
    }
}
