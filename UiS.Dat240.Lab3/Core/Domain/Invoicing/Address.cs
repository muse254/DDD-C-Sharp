using System.Collections.Generic;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing
{
    public class Address : ValueObject
    {
        public string Building { get; set; }
        public string RoomNumber { get; set; }
        // The Notes are nullable because they are not required for the creation of the Address.
        public string? Notes { get; set; }

        // This is the constructor.
        public Address(string building, string roomNumber, string? notes)
        {
            Building = building;
            RoomNumber = roomNumber;
            Notes = notes;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time.
            yield return Building;
            yield return RoomNumber;
            yield return Notes;
        }
    }
}