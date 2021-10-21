using UiS.Dat240.Lab3.SharedKernel; 

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Events
{
    public record OrderPlacedCopy : BaseDomainEvent
    {
        public OrderPlacedCopy(int orderId)
        { 
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
