using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines
{
    public class GetOffer
    {
        public record Request(int OrderId) : IRequest<Offer?>;

        public class Handler : IRequestHandler<Request, Offer?>
        {
            private readonly ShopContext _db;

            public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<Offer?> Handle(Request request, CancellationToken cancellationToken)
                => await _db.Offers.Where(offer => offer.OrderId == request.OrderId)
                                    .SingleOrDefaultAsync(cancellationToken);
        }
    }
}