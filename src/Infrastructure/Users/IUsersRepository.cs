namespace Infrastructure.Users;

public interface IUsersRepository
{
    Task Insert(UserDbo user);
    Task<UserDbo?> Get(Guid id);
    Task<UserDbo?> Get(string providerId);
}
