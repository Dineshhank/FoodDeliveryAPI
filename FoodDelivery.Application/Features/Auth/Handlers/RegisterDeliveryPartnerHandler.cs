using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Handlers
{
    public class RegisterDeliveryPartnerHandler
        : IRequestHandler<RegisterDeliveryPartnerCommand, RegisterDeliveryPartnerResult>
    {
        private const string DeliveryPartnerRoleName = "DeliveryPartner";

        private readonly IUserRepository _userRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IServiceAreaRepository _serviceAreaRepository;

        public RegisterDeliveryPartnerHandler(
            IUserRepository userRepository,
            IDeliveryRepository deliveryRepository,
            IServiceAreaRepository serviceAreaRepository)
        {
            _userRepository = userRepository;
            _deliveryRepository = deliveryRepository;
            _serviceAreaRepository = serviceAreaRepository;
        }

        public async Task<RegisterDeliveryPartnerResult> Handle(
            RegisterDeliveryPartnerCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _userRepository.GetByPhoneAsync(request.Phone);
            if (existing != null)
            {
                return new RegisterDeliveryPartnerResult
                {
                    Success = false,
                    Message = "A user with this phone number already exists"
                };
            }

            var serviceAreaId = await _serviceAreaRepository.GetFirstActiveIdAsync(cancellationToken);
            if (serviceAreaId == null)
            {
                return new RegisterDeliveryPartnerResult
                {
                    Success = false,
                    Message = "No active service area is configured. Add a service area before registering delivery partners."
                };
            }

            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Fullname = request.FullName,
                Phone = request.Phone,
                Email = null,
                Passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Isactive = true,
                Isphoneverified = true,
                Isemailverified = false,
                Isdeleted = false,
                Createdat = now
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.AssignRoleAsync(user.Id, DeliveryPartnerRoleName);

            var partner = new Deliverypartner
            {
                Id = Guid.NewGuid(),
                Userid = user.Id,
                Serviceareaid = serviceAreaId.Value,
                Vehicletype = request.VehicleType,
                Vehiclenumber = request.VehicleNumber,
                Isavailable = false,
                Createdat = now
            };

            await _deliveryRepository.AddAsync(partner, cancellationToken);
            await _userRepository.SaveChangesAsync();

            return new RegisterDeliveryPartnerResult
            {
                Success = true,
                Message = "Delivery partner registered",
                UserId = userId
            };
        }
    }
}
