using Infrastructure.Channels;

namespace Application.Channels;

public sealed record ChannelModel(Guid Id, string Name, DateTime CreatedAt)
{
    public static ChannelModel From(ChannelDbo channel) =>
        new(channel.Id, channel.Name, channel.CreatedAt);
}
