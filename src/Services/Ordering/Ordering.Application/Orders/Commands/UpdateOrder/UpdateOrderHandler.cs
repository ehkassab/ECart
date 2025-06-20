﻿
namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext context) :
        ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(request.Order.Id);
            var order = await context.Orders.FindAsync(new object[] { orderId }, cancellationToken);

            if(order == null)
            {
                throw new OrderNotFoundException(orderId.Value);
            }

            UpdateOrderWithNewValues(order,request.Order);

            context.Orders.Update(order);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order,OrderDto orderDto)
        {
            var shippingAddrs = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

            var billingAddrs = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName
                , orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: shippingAddrs,
                billingAddress: billingAddrs,
                payment: updatedPayment,
                status: orderDto.Status
                );
        }
    }
}
