using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(Refreshtoken token);
        Task<Refreshtoken?> GetByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
