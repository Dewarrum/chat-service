using Microsoft.AspNetCore.SignalR;

namespace Web.App.Hubs;

public sealed class ChatHub : Hub<IChatClient>
{
    public async Task JoinChannel(Guid channelId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
    }

    public async Task LeaveChannel(Guid channelId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelId.ToString());
    }
}
