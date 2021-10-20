using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Handlers
{

    public class ShipperAddedHandler : INotificationHandler<ShipperAdded>
    {
        private readonly ShopContext _db;

        public ShipperAddedHandler(ShopContext db)
                    => _db = db ?? throw new System.ArgumentNullException(nameof(db));

        public async Task Handle(ShipperAdded notification, CancellationToken cancellationToken)
        {
            // This should trigger an event which marks the order as sent
            var order = _db.Orders.Find(notification.OrderId);
            order.Status = Status.Shipped;

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}