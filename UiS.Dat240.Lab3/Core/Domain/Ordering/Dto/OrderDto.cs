using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


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


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto{
    public class OrderDto{
       public int Id { get; set; }
		public DateTime Date { get; set; } 

        public List<OrderLineDto> OrderLines{get;set;}  = new List<OrderLineDto>();

        public Location Location{get;set;}

        public string Notes{get;set;}

        public CustomerDto CustomerDto{get;set;}

        public StatusDto status{get;set;}

        public void OrderDto(){

        }

        public void AddOrderLine(OrderLineDto orderLine){
             OrderLines.Add(orderLine);
        }
    }
}