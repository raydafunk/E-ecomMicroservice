using System.Globalization;

namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address()
    {

    }
    private Address(string firstname, string lastname, string emailAddress, string addressline, string country, string state, string zipCode)
    {
        FirstName = firstname;
        LastName = lastname;
        EmailAddress = emailAddress;
        AddressLine = addressline;
        Country = country;
        State = state;
        ZipCode = zipCode;

    }
    public static Address Of(string firstname, string lastname, string emailAddress, string addressline, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressline);

        return new Address(firstname, lastname,  emailAddress,  addressline,  country,  state, zipCode);
    }
}
