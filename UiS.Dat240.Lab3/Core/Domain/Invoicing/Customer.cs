
namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Customer
    {
        public Customer() { }
        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;
    }
}