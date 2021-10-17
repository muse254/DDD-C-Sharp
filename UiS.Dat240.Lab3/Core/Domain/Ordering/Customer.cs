/*    
    class Customer {
        + Id
        + Name
    }
*/

using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Customer : BaseEntity
    {
        public Customer() { }
        public Customer(string name)
        {
            Name = name;
        }

        public int Id { get; protected set; }
        public string Name { get; private set; } = "";
    }
}