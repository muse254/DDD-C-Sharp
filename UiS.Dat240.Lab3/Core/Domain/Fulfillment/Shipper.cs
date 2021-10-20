/*
 class Shipper {
        + Name
    }
*/

using UiS.Dat240.Lab3.SharedKernel;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Shipper : BaseEntity
    {
        public Shipper() { }
        public Shipper(string shipperName, Offer offer)
        {
            Name = shipperName;
            Offer = Offer;

            // This should trigger an event which marks the order as sent
            Events.Add(new ShipperAdded(offer.OrderId));
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = null!;

        public int OfferId { get; set; }
        public Offer Offer { get; set; } = null!;

        public int ReimbursementId { get; set; }
        public Reimbursement Reimbursement { get; set; } = null!;
    }
}