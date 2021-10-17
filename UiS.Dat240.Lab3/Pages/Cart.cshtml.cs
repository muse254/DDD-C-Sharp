using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Cart;
using UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace UiS.Dat240.Lab3.Pages
{
    public class CartModel : PageModel
    {
        private readonly IMediator _mediator;

        public CartModel(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        public ShoppingCart? Cart { get; private set; }

        public string CustomerName { get; private set; } = "";

        public Location Location { get; private set; } = new();

        // From the the creation of the Location object and CustomerName there can be a maximum of 3 error messages
        public string[] Errors { get; private set; } = System.Array.Empty<string>();

        public async Task OnGetAsync()
        {
            var cartId = HttpContext.Session.GetGuid("CartId");
            if (cartId is null) return;

            Cart = await _mediator.Send(new Get.Request(cartId.Value));
        }

        public async Task<IActionResult> OnPostAsync(string customerName, Location location)
        {
            // get cart details
            var cartId = HttpContext.Session.GetGuid("CartId");
            if (cartId is null) throw new ArgumentNullException(nameof(cartId));
            Cart = await _mediator.Send(new Get.Request(cartId.Value));

            Location = location;
            CustomerName = customerName;

            // initialize error string array
            var errors = new List<string>();

            // return error if CustomerName and/or Building and/or RoomNumber is not set
            if (string.IsNullOrEmpty(CustomerName)) { errors.Add("Name is not set"); }
            if (string.IsNullOrEmpty(Location.Building)) { errors.Add("Building is not set"); }
            if (string.IsNullOrEmpty(Location.RoomNumber)) { errors.Add("RoomNumber is not set"); }

            if (errors.Count > 0) { Errors = errors.ToArray(); return Page(); }

            await _mediator.Send(new CartCheckout.Request(CustomerName, Location.Building, Location.RoomNumber, Location.Notes, cartId.Value));

            return RedirectToPage("Index");
        }
    }
}
