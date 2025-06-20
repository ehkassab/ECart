﻿
namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler (IApplicationDbContext context)
        : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddrs = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, 
                orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, 
                orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

            var billingAddrs = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName
                , orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, 
                orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var newOrder = Order.Create(
                orderId: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddrs,
                billingAddress: billingAddrs,
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
            );

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
            }
            return newOrder;
        }
    }
}
