namespace BankAccount.Application;

public record HolderDto(string FirstName,
    string LastName,
    string Email,
    string Phone,
    string AddressLine,
    string Country,
    string State,
    string ZipCode);
