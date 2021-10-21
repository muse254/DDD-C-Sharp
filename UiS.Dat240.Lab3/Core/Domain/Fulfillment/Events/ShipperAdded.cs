using UiS.Dat240.Lab3.SharedKernel;
using System;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events
{
    public record ShipperAdded : BaseDomainEvent
    {
        // The constructor requires that the orderId be provided.
        public ShipperAdded(Guid orderId)
        {
            OrderId = orderId;
        }

        // The OrderId is the variable used to store the orderId provided.
        public Guid OrderId { get; }
    }
}
