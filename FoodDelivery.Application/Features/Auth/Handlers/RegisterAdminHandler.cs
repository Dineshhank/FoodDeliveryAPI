using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class RegisterAdminHandler : IRequestHandler<RegisterAdminCommand, AdminRegisterResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterAdminHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AdminRegisterResponse> Handle(
            RegisterAdminCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository
                .GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                return new AdminRegisterResponse
                {
                    Success = false,
                    Message = "Admin with this email already exists"
                };
            }

            var now = DateTime.UtcNow;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Fullname = request.FullName,
                Email = request.Email,
                Passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Isactive = true,
                Isphoneverified = false,
                Isemailverified = true,
                Isdeleted = false,
            
            };

            await _userRepository.AddUserAsync(user);

            // 🔥 Assign Admin role
            await _userRepository.AssignRoleAsync(user.Id, "Admin");

            await _userRepository.SaveChangesAsync();

            return new AdminRegisterResponse
            {
                Success = true,
                Message = "Admin registered successfully"
            };
        }
    }
}
