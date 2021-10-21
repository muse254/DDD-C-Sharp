using System.Collections.Generic;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UiS.Dat240.Lab3.Pages
{
    public class OrdersModel : PageModel
    {
        // _mediator is used to store the IMediator from the OrdersModel constructor parameter.
        private readonly IMediator _mediator;

        // The constructor is instantiated and the dependency injection container includes the IMedaitor
        // to the constructor parameters.
        public OrdersModel(IMediator mediator)
            // The IMediator is stored ar _mediator, if it is null an exception is thrown.
            => _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));

        // Orders is used as the varible to store the fetched orders from the database and is aslo accessible as a models object. 
        public List<Order> Orders { get; private set; } = new List<Order>();

        public async Task OnGetAsync()
            // Fetch all placed orders and set them to Orders variable.
            => Orders = await _mediator.Send(new Core.Domain.Ordering.Pipelines.GetOrders.Request());
    }
}
