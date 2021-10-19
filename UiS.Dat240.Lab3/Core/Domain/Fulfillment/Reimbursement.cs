/*
class Reimbursement {
        + Shipper
        + Amount
        + InvoiceId
    }
*/
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Reimbursement : BaseEntity
    {
        public Reimbursement() { }
        public Reimbursement(Shipper shipper, float amount, int invoiceId)
        {
            Shipper = shipper;
            Amount = amount;
            InvoiceId = invoiceId;
        }

        public int Id { get; set; }
        public Shipper Shipper { get; set; } = new();
        public float Amount { get; set; }
        public int InvoiceId { get; set; }
    }
}