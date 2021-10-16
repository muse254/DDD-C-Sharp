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

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Order : BaseEntity
    {

        public Order() { }
        public Order(Location location, string customerName, OrderLine[] orderLines)
        {
            OrderLines = orderLines;
            Location = location;
            Customer = new Customer(customerName);
        }
        public int Id { get; protected set; }
        public DateTime Date { get; } = DateTime.Now;
        public OrderLine[]? OrderLines { get; set; }
        public Location? Location { get; protected set; }
        public string Notes { get; set; } = "";
        public Customer? Customer { get; protected set; }
        public Status Status { get; set; } = Status.New;

        public void AddOrderLine(OrderLine orderLine)
        {
            _ = OrderLines ?? throw new ArgumentNullException(nameof(OrderLines));
            OrderLines.Append(orderLine);
        }
    }
}