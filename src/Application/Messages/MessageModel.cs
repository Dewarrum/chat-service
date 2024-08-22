using Infrastructure.Messages;
using Infrastructure.Users;

namespace Application.Messages;

public sealed record MessageModel(
    Guid Id,
    Guid ChannelId,
    Guid UserId,
    string UserDisplayName,
    string UserEmail,
    string Text,
    DateTime CreatedAt
)
{
    public static MessageModel From(MessageDbo message, UserDbo user) =>
        new(
            message.Id,
            message.ChannelId,
            message.UserId,
            user.DisplayName,
            user.Email,
            message.Text,
            message.CreatedAt.UtcDateTime
        );
}
