using Infrastructure.Channels;
using Infrastructure.Messages;
using Infrastructure.Notifications;
using Infrastructure.Users;

namespace Application.Messages;

internal sealed class MessagesService(
    IMessagesRepository messagesRepository,
    IChannelsRepository channelsRepository,
    IUsersRepository usersRepository,
    INotificationService notificationService
) : IMessagesService
{
    public async Task<IReadOnlyList<MessageModel>> GetMessages(Guid channelId)
    {
        var messages = await messagesRepository.GetMany(channelId, 50);

        var userIds = messages.Select(m => m.UserId).ToHashSet();
        var getUserTasks = userIds.Select(usersRepository.Get).ToList();

        await Task.WhenAll(getUserTasks);
        var usersById = getUserTasks
            .Select(t => t.Result)
            .OfType<UserDbo>()
            .ToDictionary(u => u.Id);

        return messages.Select(m => MessageModel.From(m, usersById[m.UserId])).ToList();
    }

    public async Task CreateMessage(CreateMessageRequest request)
    {
        var channel = await channelsRepository.Get(request.ChannelId);
        if (channel is null)
        {
            throw new ChannelNotFoundException(request.ChannelId);
        }

        var user = await usersRepository.Get(request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var message = new MessageDbo
        {
            Id = Guid.NewGuid(),
            ChannelId = request.ChannelId,
            UserId = request.UserId,
            Text = request.Text,
            CreatedAt = DateTime.UtcNow
        };

        await messagesRepository.Insert(message);

        await notificationService.NotifyNewMessage(NewMessageNotification.From(message));
    }
}
