/*
 class Shipper {
        + Name
    }
*/

using UiS.Dat240.Lab3.SharedKernel;
namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Shipper : BaseEntity
    {
        public Shipper() { }
        public Shipper(string shipperName)
        {
            Name = shipperName;
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = "";

        public int OfferId { get; set; }
        public Offer Offer { get; set; } = null!;

        public int ReimbursementId { get; set; }
        public Reimbursement Reimbursement { get; set; } = null!;
    }
}