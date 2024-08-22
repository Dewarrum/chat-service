namespace Infrastructure.Channels;

public sealed class ChannelDbo
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}
