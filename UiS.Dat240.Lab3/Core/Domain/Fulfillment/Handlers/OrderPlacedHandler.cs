using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using System;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Handlers
{
    public class OrderPlacedHandler : INotificationHandler<OrderPlaced>
    {
        private readonly ShopContext _db;

        public OrderPlacedHandler(ShopContext db) => _db = db ?? throw new System.ArgumentNullException(nameof(db));

        public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken)
        {
            // When an OrderPlaced event is raised then a handler in the fulfillment context should create an empty offer
            var offer = new Offer(notification.OrderId);
            _db.Offers.Add(offer);

            // The Reimbursement should be created without InvoiceId and Shipper with only the sum of the order specified.
            var reimbursement = new Reimbursement();
            _db.Reimbursements.Add(reimbursement);

            Console.WriteLine("Empty offer created");

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}