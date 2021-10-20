using System.Collections.Generic;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;

namespace UiS.Dat240.Lab3.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IMediator _mediator;

        public OrderModel(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        public Order? Order { get; private set; }

        public string Error { get; private set; } = System.String.Empty;
        public string Shipper { get; private set; } = System.String.Empty;

        public async Task OnGetAsync()
        {
            var id = int.Parse(Request.Query["id"]);
            Order = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrder.Request(id));
        }

        public async Task<IActionResult> OnPostAsync(string shipperName)
        {
            var id = int.Parse(Request.Query["id"]);
            Order = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrder.Request(id));

            Shipper = shipperName;

            if (string.IsNullOrEmpty(Shipper))
            {
                Error = ("Shipper name not provided"); return Page();
            }

            // fetch offer
            var offer = await _mediator.Send(new Core.Domain.Fulfillment.Pipelines.GetOffer.Request(id));
            _ = offer ?? throw new System.ArgumentNullException(nameof(offer));

            var result = await _mediator.Send(new AddShipper.Request(Shipper, offer));
            if (result.Success) return RedirectToPage("/Index");

            return Page();
        }
    }
}
