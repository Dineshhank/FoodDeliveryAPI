using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class GetProfileHandler : IRequestHandler<GetProfileCommand, GetProfileResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetProfileResponse> Handle(
            GetProfileCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("User not found");

            return new GetProfileResponse
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Email = user.Email,
                Isactive = user.Isactive,
                Isphoneverified = user.Isphoneverified,
                Isemailverified = user.Isemailverified,
                Lastloginat = user.Lastloginat,
                Createdat = user.Createdat,
                Updatedat = user.Updatedat,
                Deletedat = user.Deletedat,
                Isdeleted = user.Isdeleted,
                Dateofbirth = user.Dateofbirth,
                Isprofilecompleted = user.Isprofilecompleted
            };
        }
    }
}
