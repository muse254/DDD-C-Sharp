using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


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


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto
{
    public class OrderDto
    {
        public int Guid { get; }
        public DateTime Date { get; } = DateTime.Now;
        public OrderLineDto[] OrderLines { get; }
        public Location Location { get; }
        public string Notes { get; set; } = "";
        public CustomerDto CustomerDto { get; }
        public StatusDto status { get; set; } = StatusDto.New;

        public OrderDto(Location location, string customerName, OrderLineDto[] orderLines)
        {
            Location = location;
            OrderLines = orderLines;
            CustomerDto = new CustomerDto(customerName);
        }

        public void AddOrderLine(OrderLineDto orderLine)
        { 
            OrderLines.Append(orderLine); 
        }
    }
}