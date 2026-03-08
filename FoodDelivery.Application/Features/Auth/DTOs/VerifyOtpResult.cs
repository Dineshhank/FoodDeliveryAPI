namespace FoodDelivery.Application.Features.Auth.DTOs
{
    public class VerifyOtpResult
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string? Message { get; set; }
        public AuthUserDto? User { get; set; }
    }

    public class AuthUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
