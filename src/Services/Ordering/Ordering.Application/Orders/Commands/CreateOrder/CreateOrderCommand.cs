
namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull().WithMessage("Order cannot be null.");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required.");
            RuleFor(x => x.Order.ShippingAddress).NotNull().WithMessage("ShippingAddress is required.");
            RuleFor(x => x.Order.BillingAddress).NotNull().WithMessage("BillingAddress is required.");
            RuleFor(x => x.Order.Payment).NotNull().WithMessage("Payment information is required.");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("At least one OrderItem is required.");
        }
    }
}
