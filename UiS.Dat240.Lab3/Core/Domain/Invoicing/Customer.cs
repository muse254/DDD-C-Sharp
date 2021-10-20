
namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Customer
    {
        public Customer() { }
        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = null!;
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }
}