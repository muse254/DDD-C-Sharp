// Create the order aggregated root and related classes as shown in the diagram
/*
 class Order {
        <<Aggregate root>>
        + Id
        + Date
        + OrderLines
        + Location
        + Notes
        + Customer
        + Status
        + AddOrderLine()
    }
*/

using System;
using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Order : BaseEntity
    {
        private Order()
        {
            // This default constructor is required to solve this error/requirement, it is not used directly.
            /*
            System.Exception: Could not resolve a service of type 'UiS.Dat240.Lab3.Infrastructure.Data.ShopContext' for the parameter 'db' of method 'Configure' on type 'UiS.Dat240.Lab3.Startup'.
            System.InvalidOperationException: No suitable constructor was found for entity type 'Order'. 
            The following constructors had parameters that could not be bound to properties of the entity type: 
            cannot bind 'location', 'customer', 'orderLines' in 'Order(Location location, Customer customer, OrderLine[] orderLines)'
            */
        }

        // This is the constructor
        public Order(Location location, Customer customer, OrderLine[] orderLines)
        {
            OrderLines = orderLines;
            Location = location;
            Customer = customer;
            Date = DateTime.Now;
            Status = Status.Placed;

            var id = Guid.NewGuid();
            Id = id;

            Events.Add(new OrderPlaced(Id));
            Events.Add(new OrderPlacedCopy(Id));
        }

        public Guid Id { get; private set; }

        public DateTime Date { get; private set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
        public string Notes { get; set; } = "";
        public virtual Customer Customer { get; set; } = null!;
        public Status Status { get; set; }

        // AddOrderLine is used to apped an Orderline item at the end of the OrderLines list.
        public void AddOrderLine(OrderLine orderLine)
        {
            _ = OrderLines ?? throw new ArgumentNullException(nameof(OrderLines));
            OrderLines.Append(orderLine);
        }
    }
}