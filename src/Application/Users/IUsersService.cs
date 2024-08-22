namespace Application.Users;

public interface IUsersService
{
    Task<UserModel> Get(Guid id);
    Task<UserModel> Get(string providerId);
    Task<UserModel> Create(string providerId, string displayName, string email);
}
