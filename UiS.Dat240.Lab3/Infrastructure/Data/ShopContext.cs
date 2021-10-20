using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Cart;
using UiS.Dat240.Lab3.Core.Domain.Products;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Invoicing;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment;
using CustomerInvoicing = UiS.Dat240.Lab3.Core.Domain.Invoicing.Customer;
using CustomerOrdering = UiS.Dat240.Lab3.Core.Domain.Ordering.Customer;
using UiS.Dat240.Lab3.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace UiS.Dat240.Lab3.Infrastructure.Data
{
    public class ShopContext : DbContext
    {
        private readonly IMediator _mediator;

        public ShopContext(DbContextOptions configuration, IMediator mediator) : base(configuration)
        {
            _mediator = mediator;

            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);

            // 1. Order context
            modelBuilder.Entity<Order>(entity =>
            {
                // Customer
                entity.HasOne(order => order.Customer)
                .WithOne(customer => customer.Order)
                .HasForeignKey<CustomerOrdering>(customer => customer.OrderId);

                // OrderLines
                entity.OwnsMany(order => order.OrderLines);

                // Location
                entity.OwnsOne(order => order.Location)
                .Property(location => location.Building)
                .HasColumnName("Building");
                entity.OwnsOne(order => order.Location)
                .Property(location => location.RoomNumber)
                .HasColumnName("RoomNumber");
                entity.OwnsOne(order => order.Location)
                .Property(location => location.Notes)
                .HasColumnName("LocationNotes");
            });

            // 2. Invocing context
            modelBuilder.Entity<Invoice>(entity =>
            {
                // Customer
                entity.HasOne(entity => entity.Customer)
                .WithOne(customer => customer.Invoice)
                .HasForeignKey<CustomerInvoicing>(customer => customer.InvoiceId);

                // Payment
                entity.HasOne(entity => entity.Amount)
                .WithOne(amount => amount.Invoice)
                .HasForeignKey<Payment>(amount => amount.InvoiceId);

                // Address
                entity.OwnsOne(invoice => invoice.Address)
                .Property(address => address.Building)
                .HasColumnName("Building");
                entity.OwnsOne(invoice => invoice.Address)
                .Property(address => address.RoomNumber)
                .HasColumnName("RoomNumber");
                entity.OwnsOne(invoice => invoice.Address)
                .Property(address => address.Notes)
                .HasColumnName("AddressNotes");
            });

            // 3. Fulfillement context
            // 3.1. Offer
            modelBuilder.Entity<Offer>(entity =>
                // Shipper
                entity.HasOne(entity => entity.Shipper)
                .WithOne(shipper => shipper.Offer)
                .HasForeignKey<Shipper>(shipper => shipper.OfferId)
            );

            // 3.2 Reimbursement
            modelBuilder.Entity<Reimbursement>(entity =>
                // Shipper
                entity.HasOne(entity => entity.Shipper)
                .WithOne(shipper => shipper.Reimbursement)
                .HasForeignKey<Shipper>(shipper => shipper.ReimbursementId)
            );
        }

        public DbSet<FoodItem> FoodItems { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;

        // Ordering context
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<CustomerOrdering> OrderingCustomers { get; set; } = null!;
        public DbSet<OrderLine> OrderLines { get; set; } = null!;

        // Invoicing context
        public DbSet<CustomerInvoicing> InvoicingCustomers { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;

        // Fullfilment context
        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<Reimbursement> Reimbursements { get; set; } = null!;
        public DbSet<Shipper> Shippers { get; set; } = null!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }
            }
            return result;
        }

        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
    }

    internal class FakeData
    {
        public static FoodItem[] FoodItems { get; private set; } = new FoodItem[0];

        internal static void Init()
        {
            var lorem = new Bogus.DataSets.Lorem();
            var random = new Bogus.Randomizer();
            FoodItems = Enumerable.Range(1, 30).Select(i =>
                {
                    var item = new FoodItem(lorem.Sentence(3, 2), lorem.Sentence(10, 10))
                    {
                        Price = random.Decimal(50, 250) + .99m,
                        CookTime = random.Number(5, 20)
                    };

                    // End the statement with a bang (!) to tell the compiler that the return object will never be null.
                    // We know this exists and if it actually is null then it should blow up when seeding. Something is obviously wrong then and it should fail hard. 
                    PropertyInfo propertyInfo = typeof(FoodItem).GetProperty(nameof(item.Id))!;
                    propertyInfo.SetValue(item, i);
                    return item;
                }).ToArray();

        }
    }
}
