/*
 class Shipper {
        + Name
    }
*/

using UiS.Dat240.Lab3.SharedKernel;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using System;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Shipper : BaseEntity
    {
        // This default construct is used for the creation for the its null object.
        public Shipper() { }

        // This default constructor requires that the shipperName and orderId be provided.
        public Shipper(string shipperName, Guid orderId)
        {
            Name = shipperName;

            // The orderId is used to mark the order as having a Shipper added througn the ShipperAdded event.
            Events.Add(new ShipperAdded(orderId));
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = null!;

        // The OfferId and Offer are set as nullable as they are not required for the Shipper to be constructed.
        // 
        // The OfferId and Offer are used to identify the Shipper object as part of the
        // parent class Offer when Offer is queried by EntityFramework.
        public int? OfferId { get; set; }
        public virtual Offer? Offer { get; set; } = null!;

        // The ReimbursementId and Reimbursement are set as nullable as they are not required for the Shipper to be constructed.
        // 
        // The ReimbursementId and Reimbursement are used to identify the Shipper object as part of the
        // parent class Reimbursement when Reimbursement is queried by EntityFramework.
        public int? ReimbursementId { get; set; }
        public virtual Reimbursement? Reimbursement { get; set; }
    }
}