using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Handlers
{
    public class ShipperAddedHandler : INotificationHandler<ShipperAdded>
    {
        // _db is used as the variable to store the database context for use.
        private readonly ShopContext _db;

        // This constructor is used to create the database context that is provided by the dependency injection container.
        public ShipperAddedHandler(ShopContext db)
            // if the database context is not provided, throw an exception.
            => _db = db ?? throw new System.ArgumentNullException(nameof(db));

        public async Task Handle(ShipperAdded notification, CancellationToken cancellationToken)
        {
            // This should trigger an event which marks the order as sent
            var order = _db.Orders.Find(notification.OrderId);

            // Mark the order as Shipped.
            order.Status = Status.Shipped;

            // Persist the changes to the database.
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}