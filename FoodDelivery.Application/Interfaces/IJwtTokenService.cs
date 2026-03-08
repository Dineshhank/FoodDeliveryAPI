using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IJwtTokenService
    {
        (string AccessToken, string Jti, DateTime Expiry)
        GenerateToken(Guid userId, List<string> roles);
    }
}
