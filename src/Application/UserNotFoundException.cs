namespace Application;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid userId)
        : base($"User with id {userId} not found") { }

    public UserNotFoundException(string providerId)
        : base($"User with provider id {providerId} not found") { }
}
