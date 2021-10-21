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
        // _mediator is used to store the IMediator from the OrdersModel constructor parameter.
        private readonly IMediator _mediator;

        // The constructor is instantiated and the dependency injection container includes the IMedaitor
        // to the constructor parameters.
        public CartModel(IMediator mediator)
            // The IMediator is stored ar _mediator, if it is null an exception is thrown.
            => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));


        // The ShoppingCart is used as a varible that is accessible from the OrderModel and also available
        // for mutation when fetching an order from the database.
        // It is nullable by default.
        public ShoppingCart? Cart { get; private set; }

        // The Shipper is used as a varible that is accessible from the OrderModel.
        public CheckoutForm Form { get; set; } = new();

        // The Errors is used as a varible that is accessible from the OrderModel and also available
        // for mutation when logging errors.
        // It is empty by default.
        public string[] Errors { get; private set; } = System.Array.Empty<string>();

        public async Task OnGetAsync()
        {
            // The cartId is fetched from the Session data.
            var cartId = HttpContext.Session.GetGuid("CartId");
            if (cartId is null) return;

            // If cartId is present, The Cart varible is used to store the cart fetched from the databse
            // using the cartId as the identifier.
            Cart = await _mediator.Send(new Get.Request(cartId.Value));
        }

        public async Task<IActionResult> OnPostAsync(CheckoutForm form)
        {
            // The cartId is fetched from the Session data.
            var cartId = HttpContext.Session.GetGuid("CartId");
            // if the cartId is null, throw an exception.
            if (cartId is null) throw new ArgumentNullException(nameof(cartId));
            // If cartId is present, The Cart varible is used to store the cart fetched from the databse
            // using the cartId as the identifier.
            Cart = await _mediator.Send(new Get.Request(cartId.Value));

            // Using the CheckoutForm, create a checkout request and save the response to a variable.
            var response = await _mediator.Send(new CartCheckout.Request(form.CustomerName, form.Building,
            form.RoomNumber, form.LocationNotes, cartId.Value));

            // check the success of the response data.
            if (response.Success)
            {
                return RedirectToPage("Index");
            }

            // Update the variables then reutrn the Page() with the mutated data.
            Form = form;
            Errors = response.Errors;

            return Page();
        }
    }
}
