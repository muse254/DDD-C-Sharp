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
        public Payment(float amount)
        {
            Amount = amount;
        }


        public int Id { get; set; }
        public float Amount { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }
}