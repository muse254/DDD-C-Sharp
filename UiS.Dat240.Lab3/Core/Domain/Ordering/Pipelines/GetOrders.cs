using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines
{
    public class GetOrders
    {
        // It receives no request parameters and returns a list of orders as its response.
        public record Request() : IRequest<List<Order>>;

        public class Handler : IRequestHandler<Request, List<Order>>
        {
            // _db is used as the variable to store the database context for use.
            private readonly ShopContext _db;

            // This constructor is used to create the database context that is provided by the dependency injection container.
            public Handler(ShopContext db)
                // if the database context is not provided, throw an exception.
                => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<List<Order>> Handle(Request request, CancellationToken cancellationToken)
            {
                // Include the customers from the Customer table for each order if found.
                return await _db.Orders.Include(order => order.Customer)
                                       // Include the orderLines from the orderLines table if found.
                                       .Include(order => order.OrderLines)
                                       .ToListAsync(cancellationToken);
            }
        }
    }
}