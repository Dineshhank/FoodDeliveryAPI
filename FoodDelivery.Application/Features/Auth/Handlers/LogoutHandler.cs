using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LogoutHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> Handle(
            LogoutCommand request,
            CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (token == null)
                return false;

            token.Isrevoked = true;
            token.Revokedat = DateTime.UtcNow;

            await _refreshTokenRepository.SaveChangesAsync();

            return true;
        }
    }
}
