/*
class Location {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }
*/

using System.Collections.Generic;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Location : ValueObject
    {
        public string Building { get; set; }
        public string RoomNumber { get; set; }
        // The Notes are nullable because they are not required for the creation of the Location.
        public string? Notes { get; set; }

        // This is the constructor.
        public Location(string building, string roomNumber, string? notes)
        {
            Building = building;
            RoomNumber = roomNumber;
            Notes = notes;
        }

        // This method allows for the Location object to a value object.
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time.
            yield return Building;
            yield return RoomNumber;
            yield return Notes;
        }
    }
}
