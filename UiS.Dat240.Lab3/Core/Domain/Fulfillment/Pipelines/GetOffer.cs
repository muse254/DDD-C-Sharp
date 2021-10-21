using System;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines
{
    public class GetOffer
    {
        // Request requires that an orderId is provided
        public record Request(Guid OrderId) : IRequest<Offer?>;

        public class Handler : IRequestHandler<Request, Offer?>
        {
            // _db context variable to hold the context from DI container
            private readonly ShopContext _db;

            // Fetch database context provided from the dependency injection container
            public Handler(ShopContext db)
                // throw an exception if the provided context is null
                => _db = db ?? throw new ArgumentNullException(nameof(db));

            // Allows to fetch the Offer object prvided its orderId
            public async Task<Offer?> Handle(Request request, CancellationToken cancellationToken)
                // for the offer fetched include a foreign table Shipper if found
                => await _db.Offers.Include(offer => offer.Shipper)
                                   // fetch the offer using the provided orderId as the identifier.
                                   .SingleOrDefaultAsync(offer => offer.OrderId == request.OrderId, cancellationToken);
        }
    }
}