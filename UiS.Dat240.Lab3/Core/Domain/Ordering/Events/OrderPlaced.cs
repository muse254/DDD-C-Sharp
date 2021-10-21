using UiS.Dat240.Lab3.SharedKernel;
using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Events
{
    public record OrderPlaced : BaseDomainEvent
    {
        // The constructor requires that the orderId be provided.
        public OrderPlaced(Guid orderId)
        {
            OrderId = orderId;
        }

        // The OrderId is the variable used to store the orderId provided.
        public Guid OrderId { get; }
    }
}
