namespace BankAccount.Domain;

public class Holder : Entity<HolderId>
{
    public HolderName Name { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Phone Phone { get; private set; } = default!;
    public Address Address { get; private set; } = default!;

    public static Holder Create(HolderId id, HolderName name, Email email, Phone phone, Address address)
    {
        var holder = new Holder
        {
            Id = id,
            Name = name,
            Email = email,
            Phone = phone,
            Address = address
        };

        return holder;
    }
}
