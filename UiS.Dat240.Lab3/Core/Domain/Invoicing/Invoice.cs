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

using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            // This default constructor is required to solve this error/requirement, it is not used directly.
            /*
            System.Exception: Could not resolve a service of type 'UiS.Dat240.Lab3.Infrastructure.Data.ShopContext' 
                for the parameter 'db' of method 'Configure' on type 'UiS.Dat240.Lab3.Startup'.
            System.InvalidOperationException: No suitable constructor was found for entity type 'Invoice'. 
                The following constructors had parameters that could not be bound to properties of the entity type: 
                cannot bind 'Building', 'RoomNumber', 'LocationNotes', 'customerName', 'amount' in 'Invoice(string Building, string RoomNumber, string LocationNotes, string customerName, decimal amount)'.
            */
        }
        public Invoice(
           string Building,
           string RoomNumber,
           string LocationNotes,
           string customerName,
           decimal amount
        )
        {
            Customer = new Customer(customerName);
            Address = new Address(Building, RoomNumber, LocationNotes);
            Amount = new Payment(amount);
            Status = Status.New;
        }

        public int Id { get; protected set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual Address Address { get; set; } = null!;
        public virtual Payment Amount { get; set; } = null!;
        public int OrderId { get; set; }
        public Status Status { get; set; }
    }
}