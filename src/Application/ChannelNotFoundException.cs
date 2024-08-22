namespace Application;

public sealed class ChannelNotFoundException(Guid channelId)
    : Exception($"Channel with id '{channelId}' not found");