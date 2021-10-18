/*
class Reimbursement {
        + Shipper
        + Amount
        + InvoiceId
    }
*/

using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Reimbursement : BaseEntity
    {
        public Reimbursement() { }

        public Shipper Shipper { get; set; } = new Shipper();
        public float Amount { get; set; }
        public int InvoiceId { get; set; }
    }
}