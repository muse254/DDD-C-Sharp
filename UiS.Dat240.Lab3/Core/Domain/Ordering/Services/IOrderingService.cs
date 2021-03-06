using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Services
{
    public interface IOrderingService
    {
        Task<int> PlaceOrder(Location location, string customerName, OrderLine[] orderLines);
    }

    // Create a new class which implements IOrderingService
    public class OrderingService : IOrderingService
    {
        // placing an order would require storage for the newly placed order
        // in the database; the shopcontext is therefore necesary for IOrderingService
        private readonly ShopContext _db;

        // OrderingService constructor
        public OrderingService(ShopContext db)
        {
            _db = db;
        }

        // This class implements IOrderingService
        public async Task<int> PlaceOrder(Location location, string customerName, OrderLine[] orderLines)
        {
            // create the customer making the order
            var customer = new Customer(customerName);

            // create an order form the info provided by calling the order constructor
            var order = new Order(location, customer, orderLines);

            // save the order created to the database
            _db.Orders.Add(order);

            return await _db.SaveChangesAsync();
        }
    }
}