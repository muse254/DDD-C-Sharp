using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines
{
    public class AddShipper
    {
        public record Request(
            string shipperName,
            Offer offer
        ) : IRequest<Response>;

        public record Response(bool Success, string[] Errors);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ShopContext _db;

            public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var errors = new List<string>();

                if (string.IsNullOrEmpty(request.shipperName)) errors.Add("shipperName field is required");
                if (request.offer is null) errors.Add("offer cannot be null");
                if (errors.Count > 0) return new Response(false, errors.ToArray());


                // When submitted the page should send a message to the fulfillment pipeline which assigns the shipper to the offer.
                var shipper = new Shipper(request.shipperName, request.offer ?? throw new ArgumentNullException(nameof(request.offer)));
                _db.Shippers.Add(shipper);

                await _db.SaveChangesAsync(cancellationToken);
                return new Response(true, Array.Empty<string>());
            }
        }
    }
}