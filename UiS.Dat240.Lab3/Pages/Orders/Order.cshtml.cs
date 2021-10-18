using System.Collections.Generic;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UiS.Dat240.Lab3.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IMediator _mediator;

        public OrderModel(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        public Order? Order { get; private set; }

        public async Task OnGetAsync()
        {
            var id = int.Parse(Request.Query["id"]);
            Order = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrder.Request(id));
        }
    }
}
