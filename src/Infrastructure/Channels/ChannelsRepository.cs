using Cassandra;
using Cassandra.Mapping;

namespace Infrastructure.Channels;

internal sealed class ChannelsRepository(ISession session) : IChannelsRepository
{
    private readonly IMapper _mapper = new Mapper(session);

    public async Task Insert(ChannelDbo channel)
    {
        await _mapper.InsertAsync(channel);
    }

    public async Task<IReadOnlyList<ChannelDbo>> GetAll()
    {
        var channels = await _mapper.FetchAsync<ChannelDbo>("SELECT * FROM channels");

        return channels.ToList();
    }

    public async Task<ChannelDbo?> Get(Guid id)
    {
        var channel = await _mapper.FetchAsync<ChannelDbo>(
            "SELECT * FROM channels WHERE id = ?",
            id
        );

        return channel.FirstOrDefault();
    }
}
