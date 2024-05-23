using BuildingBlocks;

namespace BankAccount.Application;
public class HolderNotFoundException(Guid id) : NotFoundException("Holder", id)
{
}
