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
    public class Offer
    {
        // This is the constructor, it requires that the orderId be provided.
        public Offer(Guid orderId)
        {
            OrderId = orderId;
        }
        public int Id { get; protected set; }
        public Guid OrderId { get; set; }

        // The Shipper is set to nullable because by default it is not provided by the constructor parameters.
        public virtual Shipper? Shipper { get; set; }
    }
}