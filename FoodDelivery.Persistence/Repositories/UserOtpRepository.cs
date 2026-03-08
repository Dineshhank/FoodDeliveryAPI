using FoodDelivery.Persistence.Context;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Application.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace FoodDelivery.Persistence.Repositories
{
    public class UserOtpRepository : IUserOtpRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public UserOtpRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddOtpAsync(Userotp otp)
        {
            await _context.Userotps.AddAsync(otp);
        }

        public async Task<Userotp?> GetValidOtpAsync(string phone, string otp)
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            return await _context.Userotps
                .Where(x => x.Phone == phone &&
                            x.Otpcode == otp &&
                            x.Isused == false &&
                            x.Expiresat > now)
                .OrderByDescending(x => x.Createdat)
                .FirstOrDefaultAsync();
        }

        public Task MarkOtpAsUsedAsync(Userotp otp)
        {
            otp.Isused = true;
            _context.Userotps.Update(otp);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
