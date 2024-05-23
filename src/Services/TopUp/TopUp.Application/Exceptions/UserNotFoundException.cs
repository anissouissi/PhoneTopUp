using BuildingBlocks;

namespace TopUp.Application;
public class UserNotFoundException(Guid id) : NotFoundException("User", id)
{
}
