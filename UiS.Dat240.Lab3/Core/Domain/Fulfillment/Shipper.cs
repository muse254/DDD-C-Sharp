/*
 class Shipper {
        + Name
    }
*/


using System.Linq;
using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment
{
    public class Shipper : BaseEntity
    {
        public Shipper() { }

        public string Name { get; set; } = "";
    }
}