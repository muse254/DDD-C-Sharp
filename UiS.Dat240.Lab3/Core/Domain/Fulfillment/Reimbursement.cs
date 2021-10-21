/*
class Reimbursement {
        + Shipper
        + Amount
        + InvoiceId
    }
*/

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Reimbursement
    {
        // This default construct is used for the creation for the its null object.
        public Reimbursement() { }

        // This constructor requires that the amount be provided.
        public Reimbursement(float amount)
        {
            Amount = amount;
        }

        public int Id { get; set; }

        // The Shipper is not provided in the constructors but when used 
        // the (null!) assures that it will not be a null object.
        public virtual Shipper Shipper { get; set; } = null!;
        public float Amount { get; set; }

        // The InvoiceId can be null as it is not required for the construction of the object.
        public int? InvoiceId { get; set; }
    }
}