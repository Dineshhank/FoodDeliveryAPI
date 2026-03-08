using FoodDelivery.Application.Interfaces;
using FoodDelivery.Persistence.Context;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Persistence.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public UserRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Phone == phone);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return Task.CompletedTask;
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            return await _context.Userroles
                .Where(ur => ur.Userid == userId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

        }

        public async Task AssignRoleAsync(Guid userId, string roleName)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);

            if (role == null)
                throw new Exception("Role not found");

            var userRole = new Userrole
            {
                Userid = userId,
                Roleid = role.Id
            };

            await _context.Userroles.AddAsync(userRole);
        }

        public async Task<(Guid RoleId, string RoleName)?> GetUserRoleByNameAsync(Guid userId, string roleName)
        {
            var role = await _context.Userroles
                .Where(ur => ur.Userid == userId &&
                             
                             ur.Role.Name == roleName)
                .Select(ur => new
                {
                    ur.Roleid,
                    ur.Role.Name
                })
                .FirstOrDefaultAsync();

            if (role == null)
                return null;

            return (role.Roleid, role.Name);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == email &&
                    u.Isactive &&
                    !u.Isdeleted);
        }

        public async Task<User?> GetByIdAsync(Guid userid)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Id == userid &&
                    u.Isactive &&
                    !u.Isdeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
