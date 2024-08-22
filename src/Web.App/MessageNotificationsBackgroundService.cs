using Confluent.Kafka;
using Infrastructure.Notifications;
using Microsoft.AspNetCore.SignalR;
using Web.App.Hubs;

namespace Web.App;

internal sealed class MessageNotificationsBackgroundService(
    IConsumer<string, NewMessageNotification> consumer,
    IHubContext<ChatHub, IChatClient> hubContext,
    ILogger<MessageNotificationsBackgroundService> logger
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        consumer.Subscribe("WebSocket.Messages");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var consumerResult = consumer.Consume(stoppingToken);
                logger.LogInformation(
                    "Received message '{Text}'",
                    consumerResult.Message.Value.Text
                );

                await hubContext
                    .Clients.Group(consumerResult.Message.Value.ChannelId.ToString())
                    .NotifyNewMessage(consumerResult.Message.Value);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ConsumeException e)
            {
                logger.LogWarning(e, "Consume error");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error while consuming");
                throw;
            }
        }
    }

    public override void Dispose()
    {
        consumer.Close();
        consumer.Dispose();

        base.Dispose();
    }
}
