using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines
{
    public class GetOrder
    {
        // Request requires that the orderId be provided in the request parameters.
        // It returns a nullable Order as its response.
        public record Request(Guid OrderId) : IRequest<Order?>;

        public class Handler : IRequestHandler<Request, Order?>
        {
            // _db is used as the variable to store the database context for use.
            private readonly ShopContext _db;

            // This constructor is used to create the database context that is provided by the dependency injection container.
            public Handler(ShopContext db)
                // if the database context is not provided, throw an exception.
                => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<Order?> Handle(Request request, CancellationToken cancellationToken)
                // Include the customers from the Customer table for the order if found.
                => await _db.Orders.Include(order => order.Customer)
                                   // Use the orderId as the identifier for the order.
                                   .Where(order => order.Id == request.OrderId)
                                   .SingleOrDefaultAsync(cancellationToken);
        }
    }
}