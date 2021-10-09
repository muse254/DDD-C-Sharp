using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Services
{
    public interface IOrderingService
    {
        Task<int> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines);
    }

    // Create a new class which implements IOrderingService
    public class OrderingService : IOrderingService
    {
      public Task<int> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines)
      { 
          // TODO: 
          var order = new OrderDto(location, customerName, orderLines);
          
          return Task.FromResult(1);
      }  
    }
}