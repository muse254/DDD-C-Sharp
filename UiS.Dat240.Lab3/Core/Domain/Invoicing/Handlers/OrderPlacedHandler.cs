using System;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing.Handlers
{
    public class OrderPlacedHandler : INotificationHandler<OrderPlaced>
    {
        private readonly ShopContext _db;

        public OrderPlacedHandler(ShopContext db)
                    => _db = db ?? throw new System.ArgumentNullException(nameof(db));

        public async Task Handle(OrderPlaced notification, CancellationToken cancellationToken)
        {
            // When an OrderPlaced event is raised then a handler in the Invoice context should create an invoice and related classes
            //

            // fetch order object
            var order = await _db.Orders.Include(order => order.Customer)
                                        .Include(order => order.OrderLines)
                                        .Where(order => order.Id == notification.OrderId)
                                        .SingleOrDefaultAsync(cancellationToken);

            decimal amount = new Decimal(0);
            // get sum of all items
            foreach (var orderline in order.OrderLines)
            {
                var value = (orderline.Price * orderline.Count);
                amount += value;
            }

            // create invoice from order Information
            var invoice = new Invoice(order.Location.Building, order.Location.RoomNumber,
                order.Location.Notes, order.Customer.Name, amount);

            // save invoice created to the database
            _db.Invoices.Add(invoice);
            await _db.SaveChangesAsync();
        }

    }
}