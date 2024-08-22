using Confluent.Kafka;

namespace Infrastructure.Notifications;

internal sealed class NotificationService(IProducer<string, NewMessageNotification> producer)
    : INotificationService
{
    public async Task NotifyNewMessage(NewMessageNotification notification)
    {
        await producer.ProduceAsync(
            "WebSocket.Messages",
            new Message<string, NewMessageNotification> { Value = notification }
        );
    }
}
