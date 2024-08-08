using CommonOperations.Messaging.Events;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Intergration
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        public Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
