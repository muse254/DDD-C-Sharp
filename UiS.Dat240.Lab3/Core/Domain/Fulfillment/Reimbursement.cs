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
        public Reimbursement() { }
        public Reimbursement(float amount)
        {
            Amount = amount;
        }

        public int Id { get; set; }
        public virtual Shipper Shipper { get; set; } = null!;
        public float Amount { get; set; }
        public int? InvoiceId { get; set; }
    }
}