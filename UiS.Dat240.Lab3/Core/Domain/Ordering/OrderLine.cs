/*
 class OrderLine{
        +Id
        +Item
        +Price
        +Count
    }
*/

using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class OrderLine
    {
        public OrderLine()
        {
            // This default constructor is required to solve this error/requirement, it is not used directly
            /*
            System.Exception: Could not resolve a service of type 'UiS.Dat240.Lab3.Infrastructure.Data.ShopContext' for the parameter 'db' of method 'Configure' on type 'UiS.Dat240.Lab3.Startup'.
            System.InvalidOperationException: No suitable constructor was found for entity type 'OrderLine'. 
            The following constructors had parameters that could not be bound to properties of the entity type: 
            cannot bind 'name' in 'OrderLine(Guid id, string name, decimal price, int count)'.
            */
        }

        // This is the contructor
        public OrderLine(Guid id, string name, decimal price, int count)
        {
            Id = id;
            Item = name;
            Price = price;
            Count = count;
        }
        public Guid Id { get; protected set; }
        public string Item { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}