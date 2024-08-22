using Cassandra;
using Cassandra.Mapping;

namespace Infrastructure.Users;

public sealed class UsersRepository(ISession session) : IUsersRepository
{
    private readonly IMapper _mapper = new Mapper(session);

    public async Task Insert(UserDbo user)
    {
        await _mapper.InsertAsync(user);
    }

    public async Task<UserDbo?> Get(Guid id)
    {
        return await _mapper.FirstOrDefaultAsync<UserDbo>(
            "SELECT * FROM users WHERE id = ? LIMIT 1",
            id
        );
    }

    public async Task<UserDbo?> Get(string providerId)
    {
        return await _mapper.FirstOrDefaultAsync<UserDbo>(
            "SELECT * FROM users_by_provider_id WHERE provider_id = ? LIMIT 1",
            providerId
        );
    }
}
