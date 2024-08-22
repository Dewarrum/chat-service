namespace Application.Messages;

public interface IMessagesService
{
    Task<IReadOnlyList<MessageModel>> GetMessages(Guid channelId);
    Task CreateMessage(CreateMessageRequest request);
}
