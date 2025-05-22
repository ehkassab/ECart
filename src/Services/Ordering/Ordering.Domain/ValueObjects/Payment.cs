
namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get;} = default!;
        public string CardHolderName { get;} = default!;
        public string ExpirationDate { get;} = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        private Payment()
        {

        }

        private Payment(string cardNumber, string cardHolderName, string expirationDate, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string cardHolderName, string expirationDate, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(cardHolderName, nameof(cardHolderName));
            ArgumentException.ThrowIfNullOrEmpty(expirationDate, nameof(expirationDate));
            ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3, nameof(cvv));
            return new Payment(cardNumber, cardHolderName, expirationDate, cvv, paymentMethod);
        }
    }
}
