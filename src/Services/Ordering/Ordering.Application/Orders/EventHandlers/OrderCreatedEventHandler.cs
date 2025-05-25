

namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderUpdateEvent>
    {
        public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
