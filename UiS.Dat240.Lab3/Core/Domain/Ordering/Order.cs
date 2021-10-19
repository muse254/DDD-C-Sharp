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
            Status = Status.New;
            Date = DateTime.Now;
        }
        public Order(Location location, Customer customer, OrderLine[] orderLines)
        {
            OrderLines = orderLines;
            Location = location;
            Customer = customer;
            Date = DateTime.Now;
            Status = Status.Placed;
        }

        public int Id { get; set; }
        public DateTime Date { get; private set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public virtual Location Location { get; set; } = null!;
        public string Notes { get; set; } = "";
        public virtual Customer Customer { get; set; } = null!;

        private Status _status;
        public Status Status
        {
            get => _status;
            set
            {
                if (_status != value && value == Status.Placed)
                {
                    Events.Add(new OrderPlaced(Id));
                }
                _status = value;
            }
        }

        public void AddOrderLine(OrderLine orderLine)
        {
            _ = OrderLines ?? throw new ArgumentNullException(nameof(OrderLines));
            OrderLines.Append(orderLine);
        }
    }
}