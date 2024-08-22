using Infrastructure.Users;

namespace Application.Users;

internal sealed class UsersService(IUsersRepository usersRepository) : IUsersService
{
    public async Task<UserModel> Get(Guid id)
    {
        var user = await usersRepository.Get(id);
        if (user is null)
        {
            throw new UserNotFoundException(id);
        }

        return UserModel.From(user);
    }

    public async Task<UserModel> Get(string providerId)
    {
        var user = await usersRepository.Get(providerId);
        if (user is null)
        {
            throw new UserNotFoundException(providerId);
        }

        return UserModel.From(user);
    }

    public async Task<UserModel> Create(string providerId, string displayName, string email)
    {
        var user = new UserDbo
        {
            Id = Guid.NewGuid(),
            ProviderId = providerId,
            Email = email,
            DisplayName = displayName,
            CreatedAt = DateTime.UtcNow
        };

        await usersRepository.Insert(user);

        return UserModel.From(user);
    }
}
