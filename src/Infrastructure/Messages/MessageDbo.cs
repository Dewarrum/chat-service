namespace Infrastructure.Messages;

public sealed class MessageDbo
{
    public Guid Id { get; init; }
    public Guid ChannelId { get; init; }
    public Guid UserId { get; init; }
    public string Text { get; init; } = default!;
    public DateTimeOffset CreatedAt { get; init; }
}
