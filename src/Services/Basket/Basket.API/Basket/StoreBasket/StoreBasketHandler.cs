
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null").NotEmpty().WithMessage("Cart cannot be empty");
            RuleFor(x => x.Cart.UserName).NotNull().WithMessage("UserName cannot be null")
                                                  .NotEmpty().WithMessage("UserName cannot be empty");
        }
    }

    public class StoreBasketHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            return new StoreBasketResult("swm");
        }
    }
}
