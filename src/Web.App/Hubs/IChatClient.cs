using Infrastructure.Notifications;

namespace Web.App.Hubs;

public interface IChatClient
{
    Task NotifyNewMessage(NewMessageNotification notification);
}
