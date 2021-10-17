using System.Collections.Generic;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UiS.Dat240.Lab3.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IMediator _mediator;

        public OrdersModel(IMediator mediator) => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        public List<Order> Orders { get; private set; } = new List<Order>();

        public async Task OnGetAsync()
        // Fetch all placed orders and set them to Orders variable
            => Orders = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrders.Request());
    }
}
