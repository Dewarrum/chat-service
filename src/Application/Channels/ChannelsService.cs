using Infrastructure.Channels;

namespace Application.Channels;

internal sealed class ChannelsService(IChannelsRepository repository) : IChannelsService
{
    public async Task<IReadOnlyList<ChannelModel>> GetAll()
    {
        var channels = await repository.GetAll();
        return channels.Select(ChannelModel.From).ToList();
    }

    public async Task<ChannelModel> Get(Guid id)
    {
        var channel = await repository.Get(id);
        if (channel == null)
        {
            throw new ChannelNotFoundException(id);
        }

        return ChannelModel.From(channel);
    }

    public async Task Create(CreateChannelRequest request)
    {
        var channel = new ChannelDbo
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow
        };

        await repository.Insert(channel);
    }
}
