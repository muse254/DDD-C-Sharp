/*

classDiagram
    direction LR
    class Invoice {
        <<Aggregate root>>
        + Customer
        + Address
        + Amount
        + OrderId
        + Status
    }
    class Payment {
        + Amount
    }
    class Customer {
        + Name
    }
    class Status {
        <<Enum>>
        New
        Paid
        Overdue
        Credited
    }
    class Address {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }

    Invoice --> Payment
    Invoice --> Customer
    Invoice --> Status
    Invoice --> Address
*/


using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;


namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            Status = Status.New;
        }

        public Customer Customer { get; set; } = new Customer();
        public Address Address { get; set; } = new Address();
        public float Amount { get; set; }
        public int OrderId { get; set; }
        public Status Status { get; set; }
    }
}