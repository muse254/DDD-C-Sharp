using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Services;
using System.Collections.Generic;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines
{
    /*
    Create a new Pipeline for CartCheckout (in the Cart context) which retrieves a cart and inserts it into the IOrderingService to create an order
    The current cart should be closed and removed from the session after it is placed.
    */
    public class CartCheckout
    {
        /*
        This ui has to contain the following information for placing the order:
            Building
            Room number
            Location Notes
            Customer Name
        */
        public record Request(
            string CustomerName,
            string Building,
            string RoomNumber,
            string LocationNotes,
            Guid CartId) : IRequest<Response>;

        public record Response(bool Success, string[] Errors);

        public class Handler : IRequestHandler<Request, Response>
        {

            private readonly ShopContext _db;

            private readonly IOrderingService _orderingService;

            public Handler(ShopContext db)
            {
                _db = db ?? throw new ArgumentNullException(nameof(db));
                _orderingService = new OrderingService(_db);
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var errors = new List<string>();

                // check for empty or null values in request
                if (string.IsNullOrEmpty(request.CustomerName)) errors.Add("CustomerName field is required");
                if (string.IsNullOrEmpty(request.Building)) errors.Add("Building field is required");
                if (string.IsNullOrEmpty(request.RoomNumber)) errors.Add("RoomNumber field is required");
                if (errors.Count > 0) return new Response(false, errors.ToArray());

                // retrieve the cart
                var cart = await _db.ShoppingCarts.Include(c => c.Items)
                                         .Where(c => c.Id == request.CartId)
                                         .SingleOrDefaultAsync(cancellationToken);

                // insert the cart into the IOrderingService to create an order
                if (cart != null)
                {
                    var location = new Location(request.Building, request.RoomNumber, request.LocationNotes);
                    var orderLines = new List<OrderLine>();

                    foreach (var item in cart.Items)
                    {
                        orderLines.Add(new OrderLine(item.Id, item.Name, item.Price, item.Count));
                    }

                    await _orderingService.PlaceOrder(location, request.CustomerName, orderLines.ToArray());

                    // the cart should be closed and removed from the session after it is placed
                    _db.ShoppingCarts.Remove(cart);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                return new Response(true, Array.Empty<string>());
            }
        }
    }
}