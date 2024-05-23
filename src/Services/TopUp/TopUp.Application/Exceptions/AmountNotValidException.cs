namespace TopUp.Application;

public class AmountNotValidException(string message) : Exception(message)
{
}