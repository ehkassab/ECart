
namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order Order) : IDomainEvents;
}
