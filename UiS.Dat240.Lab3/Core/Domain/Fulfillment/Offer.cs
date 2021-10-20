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


using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Offer : BaseEntity
    {
        public Offer() { }
        public Offer(int orderId)
        {
            OrderId = orderId;
        }
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Shipper Shipper { get; set; } = null!;
    }
}