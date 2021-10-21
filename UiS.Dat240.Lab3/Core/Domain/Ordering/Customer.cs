/*    
    class Customer {
        + Id
        + Name
    }
*/

using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Customer
    {
        public Customer()
        {
            // This default constructor explicitly solves this error/requirement, it is not used directly.
            /*
            System.Exception: Could not resolve a service of type 'UiS.Dat240.Lab3.Infrastructure.Data.ShopContext' for the parameter 'db' of method 'Configure' on type 'UiS.Dat240.Lab3.Startup'.
            System.InvalidOperationException: No suitable constructor was found for entity type 'Customer'. 
            The following constructors had parameters that could not be bound to properties of the entity type: 
            cannot bind 'customerName' in 'Customer(string customerName)'.
            */
        }
        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = null!;

        // The OrderId and Order are used to identify the Customer object as part of the
        // parent class Order when Order is queried by EntityFramework.
        public Guid OrderId { get; private set; }
        public Order Order { get; set; } = null!;
    }
}