using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events
{
    public record ShipperAdded : BaseDomainEvent
    {
        public ShipperAdded(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
