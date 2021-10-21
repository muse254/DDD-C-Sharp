/*
class Payment {
        + Amount
    }
*/

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Payment
    {
        public Payment() { }
        public Payment(decimal amount)
        {
            Amount = amount;
        }


        public int Id { get; protected set; }
        public decimal Amount { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;
    }
}