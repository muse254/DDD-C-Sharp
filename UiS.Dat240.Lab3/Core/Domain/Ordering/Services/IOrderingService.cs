using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Services
{
    public interface IOrderingService
    {
        Task<int> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines);
    }

// Create the order aggregated root and related classes as shown in the diagram; Order is created at Dto/Order.cs
// Create a new class which implements IOrderingService
    public class OrderingService : IOrderingService
    {
      public Task<int> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines)
      {
          // TODO:
      }  
    }
}