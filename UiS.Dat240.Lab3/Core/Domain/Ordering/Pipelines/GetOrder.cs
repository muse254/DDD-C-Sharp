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
        public record Request(int OrderId) : IRequest<Order?>;

        public class Handler : IRequestHandler<Request, Order?>
        {
            private readonly ShopContext _db;

            public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<Order?> Handle(Request request, CancellationToken cancellationToken)
                => await _db.Orders.Include(order => order.Customer)
                                   .Where(order => order.Id == request.OrderId)
                                   .SingleOrDefaultAsync(cancellationToken);
        }
    }
}