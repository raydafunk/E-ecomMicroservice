using CommonOperations.Execeptions;

namespace Ordering.Application.Execeptions;

public class OrderNotFoundException : NotFoundExecption
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
            
    }
}
