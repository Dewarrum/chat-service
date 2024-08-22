using Infrastructure.Messages;

namespace Infrastructure.Notifications;

public sealed record NewMessageNotification(
    Guid Id,
    Guid ChannelId,
    Guid UserId,
    string Text,
    DateTime CreatedAt
)
{
    public static NewMessageNotification From(MessageDbo message) =>
        new(
            message.Id,
            message.ChannelId,
            message.UserId,
            message.Text,
            message.CreatedAt.UtcDateTime
        );
}
