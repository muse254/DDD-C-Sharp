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

    public class CheckoutForm
    {
        public CheckoutForm() { }

        public string CustomerName { get; set; } = "";
        public string Building { get; set; } = "";
        public string RoomNumber { get; set; } = "";
        public string LocationNotes { get; set; } = "";
    }

    public class CartModel : PageModel
    {
        private readonly IMediator _mediator;

        public CartModel(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        public ShoppingCart? Cart { get; private set; }

        public CheckoutForm Form { get; set; } = new();

        // From the the creation of the Location object and CustomerName there can be a maximum of 3 error messages
        public string[] Errors { get; private set; } = System.Array.Empty<string>();

        public async Task OnGetAsync()
        {
            var cartId = HttpContext.Session.GetGuid("CartId");
            if (cartId is null) return;

            Cart = await _mediator.Send(new Get.Request(cartId.Value));
        }

        public async Task<IActionResult> OnPostAsync(CheckoutForm form)
        {
            // get cart details
            var cartId = HttpContext.Session.GetGuid("CartId");
            if (cartId is null) throw new ArgumentNullException(nameof(cartId));
            Cart = await _mediator.Send(new Get.Request(cartId.Value));

            var response = await _mediator.Send(new CartCheckout.Request(form.CustomerName, form.Building,
            form.RoomNumber, form.LocationNotes, cartId.Value));
            if (response.Success) return RedirectToPage("Index");

            Form = form;
            Errors = response.Errors;

            return Page();
        }
    }
}
