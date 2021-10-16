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
		private readonly ShopContext _db;

		public FoodItemPriceChangedHandler(ShopContext db)
			=> _db = db ?? throw new System.ArgumentNullException(nameof(db));

		public async Task Handle(FoodItemPriceChanged notification, CancellationToken cancellationToken)
		{
			var carts = await _db.ShoppingCarts.Include(c => c.Items)
							.Where(c => c.Items.Any(i => i.Sku == notification.ItemId))
							.ToListAsync(cancellationToken);
			foreach (var cart in carts)
			{
				foreach (var item in cart.Items.Where(i => i.Sku == notification.ItemId))
				{
					item.Price = notification.NewPrice;
				}
			}
			await _db.SaveChangesAsync(cancellationToken);
		}
	}
}
