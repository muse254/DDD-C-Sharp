/*
 class OrderLine{
        +Id
        +Item
        +Price
        +Count
    }
*/

using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class OrderLine : BaseEntity
    {
        public OrderLine() { }
        public OrderLine(Guid id, string name, decimal price, int count)
        {
            Id = id;
            Item = name;
            Price = price;
            Count = count;
        }
        public Guid Id { get; protected set; }
        public string Item { get; private set; } = "";
        public decimal Price { get; private set; }
        public int Count { get; private set; }
    }
}