/*
class Payment {
        + Amount
    }
*/

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Payment
    {
        // This is the constructor with the amount as a required parameter.
        public Payment(decimal amount)
        { 
            Amount = amount;
        }

        public int Id { get; protected set; }
        public decimal Amount { get; set; }

        // The InvoiceId and Invoice are used to identify the Payment object as part of the
        // parent class Invoice when Invoice is queried by EntityFramework.
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;
    }
}