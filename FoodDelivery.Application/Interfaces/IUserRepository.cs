using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByPhoneAsync(string phone);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task SaveChangesAsync();
        Task<List<string>> GetUserRolesAsync(Guid userId);
        Task AssignRoleAsync(Guid userId, string roleName);
        Task<(Guid RoleId, string RoleName)?> GetUserRoleByNameAsync(Guid userId, string roleName);
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(Guid userid);
    }
}
