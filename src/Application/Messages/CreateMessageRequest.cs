namespace Application.Messages;

public sealed record CreateMessageRequest(Guid ChannelId, Guid UserId, string Text);
