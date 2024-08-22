namespace Infrastructure.Notifications;

public interface INotificationService
{
    Task NotifyNewMessage(NewMessageNotification notification);
}
