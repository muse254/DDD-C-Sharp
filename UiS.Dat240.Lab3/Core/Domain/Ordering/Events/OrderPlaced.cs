using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Events
{
    public record OrderPlaced : BaseDomainEvent
    {
        public OrderPlaced(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
