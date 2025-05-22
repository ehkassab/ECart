
namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string AddressLine { get; init; } = default!;
        public string State { get; init; } = default!;
        public string ZipCode { get; init; } = default!;
        public string Country { get; init; } = default!;

        protected Address()
        {
            
        }

        private Address(string firstName,string lastName,string emailAddress,string addressLine, string state,string zipCode,string country)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string state, string zipCode, string country)
        {
            ArgumentException.ThrowIfNullOrEmpty(emailAddress, nameof(emailAddress));
            ArgumentException.ThrowIfNullOrEmpty(addressLine, nameof(addressLine));
            return new Address(firstName, lastName, emailAddress, addressLine, state, zipCode, country);
        }
    }
}
