using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines
{
    public class OfferShipperSet
    {
        public record Request(
            // require request to have a shipper name
            string shipperName,
            // require request to provide an orderId
            Guid orderId
        ) : IRequest<Response>;

        public record Response(bool Success, string[] Errors);

        public class Handler : IRequestHandler<Request, Response>
        {
            // _db context variable to hold the context from DI container
            private readonly ShopContext _db;

            // Fetch database context provided from the dependency injection container
            public Handler(ShopContext db)
                // throw an exception if the provided context is null
                => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var errors = new List<string>();

                // check whether the shippername is provided
                if (string.IsNullOrEmpty(request.shipperName)) errors.Add("shipperName field is required");
                // check whether the orderId is provided
                if (Guid.Empty == (request.orderId)) errors.Add("orderId cannot be empty");

                // if errors were present, return a Success false response with errors
                if (errors.Count > 0) return new Response(false, errors.ToArray());

                // fetch offer using the orderId as it's identifier
                var offer = await _db.Offers.SingleOrDefaultAsync(offer => offer.OrderId == request.orderId, cancellationToken);
                // throw an exception if the offer was not found 
                _ = offer ?? throw new ArgumentNullException(nameof(offer));

                // When submitted the page should send a message to the fulfillment pipeline which assigns the shipper to the offer.
                // 1. fetch offer 
                var shipper = new Shipper(request.shipperName, request.orderId);
                // 2. update the shipper with the offer.
                offer.Shipper = shipper;

                // persist the changes to the database.
                await _db.SaveChangesAsync(cancellationToken);

                // return a success respose with no errors.
                return new Response(true, Array.Empty<string>());
            }
        }
    }
}