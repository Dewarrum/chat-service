namespace Infrastructure.Messages;

public interface IMessagesRepository
{
    Task Insert(MessageDbo message);
    Task<IReadOnlyList<MessageDbo>> GetMany(Guid channelId, int limit);
}
