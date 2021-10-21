/*    
    class Customer {
        + Id
        + Name
    }
*/

using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
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
        public int OrderId { get; private set; }
        public Order Order { get; set; } = null!;
    }
}