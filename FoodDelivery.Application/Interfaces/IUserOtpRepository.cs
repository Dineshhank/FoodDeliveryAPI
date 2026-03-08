using FoodDelivery.Domain.Entities;
namespace FoodDelivery.Application.Interfaces
{
    public interface IUserOtpRepository
    {
        Task AddOtpAsync(Userotp otp);
        Task<Userotp?> GetValidOtpAsync(string phone, string otp);
        Task MarkOtpAsUsedAsync(Userotp otp);
        Task SaveChangesAsync();
    }
}
