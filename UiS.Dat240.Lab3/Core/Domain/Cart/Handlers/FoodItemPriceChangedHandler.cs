using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Products.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UiS.Dat240.Lab3.Core.Domain.Cart.Handlers
{
    // If the price of a product changes, we want to update that item in existing shopping carts.
    public class FoodItemPriceChangedHandler : INotificationHandler<FoodItemPriceChanged>
    {
        // _db is used as the variable to store the database context for use.
        private readonly ShopContext _db;

        // This constructor is used to create the database context that is provided by the dependency injection container.
        public FoodItemPriceChangedHandler(ShopContext db)
            // if the database context is not provided, throw an exception.
            => _db = db ?? throw new System.ArgumentNullException(nameof(db));

        public async Task Handle(FoodItemPriceChanged notification, CancellationToken cancellationToken)
        {
            // Fetch all the carts present.
            var carts = await _db.ShoppingCarts.Include(c => c.Items)
                            .Where(c => c.Items.Any(i => i.Sku == notification.ItemId))
                            .ToListAsync(cancellationToken);

            // For each cart
            foreach (var cart in carts)
            {
                // Check whether any of the cart item match that from the notification where
                // the itemId is the identifier.
                foreach (var item in cart.Items.Where(i => i.Sku == notification.ItemId))
                {
                    // if found, update the cart item's price to the new price.
                    item.Price = notification.NewPrice;
                }
            }

            // Persist the changes made to the database.
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
