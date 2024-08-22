namespace Application.Channels;

public interface IChannelsService
{
    Task<IReadOnlyList<ChannelModel>> GetAll();
    Task<ChannelModel> Get(Guid id);
    Task Create(CreateChannelRequest request);
}
