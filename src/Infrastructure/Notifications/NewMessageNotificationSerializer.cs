using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace Infrastructure.Notifications;

internal sealed class NewMessageNotificationSerializer
    : ISerializer<NewMessageNotification>,
        IDeserializer<NewMessageNotification>
{
    public byte[] Serialize(NewMessageNotification data, SerializationContext context)
    {
        var json = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(json);
    }

    public NewMessageNotification Deserialize(
        ReadOnlySpan<byte> data,
        bool isNull,
        SerializationContext context
    )
    {
        var json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<NewMessageNotification>(json)!;
    }
}
