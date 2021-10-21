
namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Customer
    {
        public Customer()
        {
            // This default constructor is required to solve this error/requirement, it is not used directly.
            /*
            System.Exception: Could not resolve a service of type 'UiS.Dat240.Lab3.Infrastructure.Data.ShopContext' 
            for the parameter 'db' of method 'Configure' on type 'UiS.Dat240.Lab3.Startup'.
            System.InvalidOperationException: No suitable constructor was found for entity type 'Customer'. 
            The following constructors had parameters that could not be bound to properties of the entity type: 
            cannot bind 'customerName' in 'Customer(string customerName)'.
            */
        }

        // This constructor requires that the customerName be provided in the constructor parameters.
        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Id { get; protected set; }
        public string Name { get; set; } = null!;

        // The InvoiceId and Invoice are used to identify the Customer object as part of the
        // parent class Invoice when Invoice is queried by EntityFramework.
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;
    }
}