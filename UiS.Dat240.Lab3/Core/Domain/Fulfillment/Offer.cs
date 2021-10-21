/*
classDiagram
    direction LR
    class Offer {
        <<Aggregate root>>
        + OrderId
        + Shipper
    }
    class Shipper {
        + Name
    }
    class Reimbursement {
        + Shipper
        + Amount
        + InvoiceId
    }
    Offer --> Shipper
    Offer --> Reimbursement
*/

using UiS.Dat240.Lab3.SharedKernel;
using System;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Offer : BaseEntity
    {
        public Offer()
        {
            Shipper = new();
        }
        public Offer(int orderId)
        {
            Console.WriteLine("Offer created for id: " + orderId);
            OrderId = orderId;
            Shipper = new();
        }
        public int Id { get; protected set; }
        public int OrderId { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}