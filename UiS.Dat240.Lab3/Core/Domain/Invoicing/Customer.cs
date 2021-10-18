/*
class Customer {
        + Name
    }
*/

using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;


namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Customer : BaseEntity
    {
        public Customer() { }

        public string Name { get; set; } = "";

    }
}