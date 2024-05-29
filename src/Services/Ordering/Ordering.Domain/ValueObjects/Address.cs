using System.Globalization;

namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FristName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address()
    {

    }
    private Address(string fristname, string lastname, string emailAddress, string addressline, string country, string state, string zipCode)
    {
        FristName = fristname;
        LastName = lastname;
        EmailAddress = emailAddress;
        AddressLine = addressline;
        Country = country;
        State = state;
        ZipCode = zipCode;

    }
    public static Address of(string fristname, string lastname, string emailAddress, string addressline, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressline);

        return new Address(fristname, lastname,  emailAddress,  addressline,  country,  state, zipCode);
    }
}
