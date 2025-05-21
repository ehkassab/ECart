
namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get;} = default!;
        public string CardHolderName { get;} = default!;
        public string ExpirationDate { get;} = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;
    }
}
