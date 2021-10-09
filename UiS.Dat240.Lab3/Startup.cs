using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UiS.Dat240.Lab3.Core.Domain.Products;
using UiS.Dat240.Lab3.Infrastructure.Data;
using UiS.Dat240.Lab3.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Services;

namespace UiS.Dat240.Lab3
{
    public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// We want to use the ASP.NET Session State to hold an ID for our shopping cart. There are many ways to do
			// this, some bad, some very good. For production use you would use a different storage mechanism than MemoryCache
			// See https://docs.microsoft.com/en-us/previous-versions/aspnet/ms178581(v=vs.100) for more information about Session storage
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(60); // We're keeping this low to facilitate testing. Would normally be higher. Default is 20 minutes
				options.Cookie.IsEssential = true;              // Otherwise we need cookie approval
			});

            services.AddHttpContextAccessor();

			// Register the database context
			services.AddDbContext<ShopContext>(options =>
			{
				options.UseSqlite($"Data Source={Path.Combine("Infrastructure", "Data", "shop.db")}");
			});

			// MediatR is used to dispatch requests to our domain. See https://github.com/jbogard/MediatR for more information.
			services.AddMediatR(typeof(Startup));

			// We're using Scrutor extension methods to find all classes that implement IValidator<T> and register them to the service collecion.
			// That means we can request IValidator<FoodItem> in our constructor and get delivered the FoodItemValidator implementation, since that
			// is the only class that implements IValidator<FoodItem>
			services.Scan(scan => scan
				.FromCallingAssembly()
					.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
					.AsImplementedInterfaces());

			/*var validators = typeof(Startup).Assembly.GetTypes()
								.Select(type => (Type: type, Interface: type.GetInterface(typeof(IValidator<>).FullName!)))
								.Where(type => type.Interface != null);

			foreach(var type in validators)
				services.AddSingleton(type.Interface!, type.Type);*/

			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopContext db)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				// Add some fake data for testing. Testing an app without some basic test data is dreadful.
				// Here we check if the database is empty, if it is, we add some random test data. Makes for
				// a nicer "git clone - run" experience.
				if (!db.FoodItems.Any())
				{
					FakeData.Init();
					db.FoodItems.AddRange(FakeData.FoodItems);
					db.SaveChanges();
				}
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();


			// Since the app is in English, we'll set the culture of the app to be in
			// English as well. This affects number and date formatting.
			// See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-5.0
			// for more info on globalization in ASP.NET Core.
			var supportedCultures = new[]
			{
				new CultureInfo("en-GB"),
			};
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-GB"),
				SupportedCultures = supportedCultures,
				SupportedUICultures = supportedCultures
			});

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			// Needed to add the Session handling middleware.
			app.UseSession();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
