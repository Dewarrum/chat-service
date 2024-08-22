using Cassandra;
using Cassandra.Mapping;

namespace Infrastructure.Messages;

internal sealed class MessageRepository(ISession session) : IMessagesRepository
{
    private readonly IMapper _mapper = new Mapper(session);

    public async Task Insert(MessageDbo message)
    {
        await _mapper.InsertAsync(message);
    }

    public async Task<IReadOnlyList<MessageDbo>> GetMany(Guid channelId, int limit)
    {
        var messages = await _mapper.FetchAsync<MessageDbo>(
            "SELECT * FROM messages WHERE channel_id = ? LIMIT ?",
            channelId,
            limit
        );

        return messages.ToList();
    }
}
