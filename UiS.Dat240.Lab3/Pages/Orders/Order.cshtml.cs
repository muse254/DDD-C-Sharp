using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UiS.Dat240.Lab3.Pages
{
    public class OrderModel : PageModel
    {
        // _mediator is used to store the IMediator from the OrdersModel constructor parameter.
        private readonly IMediator _mediator;

        // The constructor is instantiated and the dependency injection container includes the IMedaitor
        // to the constructor parameters.
        public OrderModel(IMediator mediator)
            // The IMediator is stored ar _mediator, if it is null an exception is thrown.
            => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        // The Order is used as a varible that is accessible from the OrderModel and also available
        // for mutation when fetching an order from the database.
        // It is nullable by default.
        public Order? Order { get; private set; }

        // The Offer is used as a varible that is accessible from the OrderModel and also available
        // for mutation when fetching an offer from the database.
        // It is nullable by default.
        public Offer? Offer { get; private set; }

        // The Errors is used as a varible that is accessible from the OrderModel and also available
        // for mutation when logging errors.
        // It is empty by default.
        public List<String> Errors { get; private set; } = new List<String>();

        // The Shipper is used as a varible that is accessible from the OrderModel.
        public Shipper Shipper { get; private set; } = new();

        // Shipped and Placed provide the status values as accessible from the OrderModel
        // for comparison operations.
        public Status Shipped = Status.Shipped;
        public Status Placed = Status.Placed;

        public async Task OnGetAsync()
        {
            // Fetch the order id from the request query parameters.
            var id = Guid.Parse(Request.Query["id"]);
            // Using the fetched id, fetch an order from the database using the orderId as the request parameter.
            // Store the fetched order to the Order variable.
            Order = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrder.Request(id));

            // Check if the Order is null, if null throw an error.
            _ = Order ?? throw new System.ArgumentNullException(nameof(Order));

            // Using the fetched id, fetch an offer from the database using the orderId as the request parameter.
            // Store the fetched offer to the Offer variable.
            Offer = await _mediator.Send(new Core.Domain.Fulfillment.Pipelines.GetOffer.Request(id));
        }

        public async Task<IActionResult> OnPostAsync(Shipper shipper)
        {
            // Fetch the order id from the request query parameters.
            var id = Guid.Parse(Request.Query["id"]);
            // Using the fetched id, fetch an order from the database using the orderId as the request parameter.
            // Store the fetched order to the Order variable.
            Order = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrder.Request(id));

            // Save the shipper variable's value to the Shipper variable.
            Shipper = shipper;

            // If the shipper name is not provided, Add an error to the list and return the Page to render the error message.
            if (string.IsNullOrEmpty(Shipper.Name))
            {
                Errors.Add("Shipper name not provided");
                return Page();
            }

            // Using the shippername and orderId as parameters call the OfferShipperSet Pipeline to
            // set the offer to the Shipper to be createdf from the shipper name.
            var result = await _mediator.Send(new OfferShipperSet.Request(Shipper.Name, id));
            if (result.Success!) Errors = result.Errors.ToList();

            // Using the fetched id, fetch an offer from the database using the orderId as the request parameter.
            // Store the fetched offer to the Offer variable.
            Offer = await _mediator.Send(new Core.Domain.Fulfillment.Pipelines.GetOffer.Request(id));

            // Reurn the page, with the modified variables.
            return Page();
        }
    }
}
