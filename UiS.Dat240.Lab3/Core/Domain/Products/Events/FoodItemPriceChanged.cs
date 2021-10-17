using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Products.Events
{
    public record FoodItemPriceChanged : BaseDomainEvent
    {
        public FoodItemPriceChanged(int itemId, decimal oldPrice, decimal newPrice)
        {
            ItemId = itemId;
            if (oldPrice >= 0)
            {
                OldPrice = oldPrice;
            }
            else
            {
                // throw an out of range exception as the price cannot be negative
                throw new System.ArgumentOutOfRangeException(nameof(oldPrice), "the price cannot be negative");
            }

            if (newPrice > 0)
            {
                NewPrice = newPrice;
            }
            else
            {
                // throw an out of range exception as the price cannot be negative
                throw new System.ArgumentOutOfRangeException(nameof(newPrice), "the price cannot be zero or negative");
            }
        }

        public int ItemId { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
    }
}