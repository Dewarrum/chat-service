namespace Infrastructure.Channels;

public interface IChannelsRepository
{
    Task Insert(ChannelDbo channel);
    Task<IReadOnlyList<ChannelDbo>> GetAll();
    Task<ChannelDbo?> Get(Guid id);
}
