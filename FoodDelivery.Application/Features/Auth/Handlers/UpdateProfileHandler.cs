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
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(
            UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("User not found");

            // Update only if value provided
            if (!string.IsNullOrWhiteSpace(request.Fullname))
                user.Fullname = request.Fullname;

            if (request.Dateofbirth.HasValue)
                user.Dateofbirth = request.Dateofbirth;

            user.Isprofilecompleted = true;
            user.Updatedat = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
