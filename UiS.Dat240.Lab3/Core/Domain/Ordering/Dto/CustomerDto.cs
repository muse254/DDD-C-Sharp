using System;

/*    class Customer {
        + Id
        + Name
    }
*/


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto{
    public class CustomerDto{
        public Guid Id { get; }
		public string Name { get; } = "";

        public CustomerDto(string name)
        {
            Name = name;
        }

    }
}