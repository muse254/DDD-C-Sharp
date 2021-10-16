/*
class Location {
        <<ValueObject>>
        + Building
        + RoomNumber
        + Notes
    }
*/

using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering
{
    public class Location : BaseEntity
    {
        public Location(string building, string roomNumber, string notes)
        {
            Building = building;
            RoomNumber = roomNumber;
            Notes = notes;
        }

        public int Id { get; protected set; }

        public string Building { get; protected set; }
        public string RoomNumber { get; protected set; }
        public string Notes { get; protected set; }
    }
}
