using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Handlers
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IOrderRepository _repo;

        public UpdateOrderStatusHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _repo.GetOrderByIdAsync(request.OrderId);

            if (order == null)
                return false;

            order.Status = request.Status;

            await _repo.UpdateOrderAsync(order);

            // 🔥 Add history
            await _repo.AddOrderStatusHistoryAsync(new Orderstatushistory
            {
                Id = Guid.NewGuid(),
                Orderid = order.Id,
                Status = request.Status,
                Changedat = DateTime.UtcNow
            });

            await _repo.SaveChangesAsync();

            return true;
        }
    }
}
