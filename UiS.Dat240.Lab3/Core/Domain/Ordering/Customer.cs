/*    
    class Customer {
        + Id
        + Name
    }
*/

using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Customer : BaseEntity
    {
        public Customer() { }
        public Customer(string customerName)
        {
            Name = customerName;
        }

        public int Id { get; private set; }
        public string Name { get; set; } = "";
        public int OrderId { get; private set; }
        public Order Order { get; set; } = null!;
    }
}