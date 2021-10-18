/*
class Address {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }
*/

using UiS.Dat240.Lab3.SharedKernel;
using System.Collections.Generic;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Address : ValueObject
    {
        public Address() { }

        public Address(string building, string roomNumber, string notes)
        {
            Building = building;
            RoomNumber = roomNumber;
            Notes = notes;
        }

        public string Building { get; private set; } = "";
        public string RoomNumber { get; private set; } = "";
        public string Notes { get; private set; } = "";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Building;
            yield return RoomNumber;
            yield return Notes;
        }
    }
}