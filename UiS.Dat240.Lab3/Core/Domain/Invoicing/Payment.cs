/*
class Payment {
        + Amount
    }
*/

using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;


namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Payment : BaseEntity
    {
        public Payment() { }

        public float Amount { get; set; }

    }
}