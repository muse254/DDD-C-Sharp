using System.Threading.Tasks;

/*
 class OrderLine{
        +Id
        +Item
        +Price
        +Count
    }
*/

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto
{
    public record OrderLineDto
    (
        int Id,
        string Item,
        decimal Price,
        int Count
    );
}