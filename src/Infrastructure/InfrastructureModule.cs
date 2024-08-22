using Cassandra;
using Cassandra.Mapping;
using Cassandra.Serialization;
using Confluent.Kafka;
using Infrastructure.Channels;
using Infrastructure.Messages;
using Infrastructure.Notifications;
using Infrastructure.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    static InfrastructureModule()
    {
        MappingConfiguration.Global.Define(
            new Map<ChannelDbo>()
                .TableName("channels")
                .PartitionKey(x => x.Id)
                .Column(x => x.Id, cm => cm.WithName("id"))
                .Column(x => x.Name, cm => cm.WithName("name"))
                .Column(x => x.CreatedAt, cm => cm.WithName("created_at"))
        );
        MappingConfiguration.Global.Define(
            new Map<MessageDbo>()
                .TableName("messages")
                .PartitionKey(x => x.ChannelId)
                .ClusteringKey(x => x.CreatedAt)
                .Column(x => x.Id, cm => cm.WithName("id"))
                .Column(x => x.ChannelId, cm => cm.WithName("channel_id"))
                .Column(x => x.UserId, cm => cm.WithName("user_id"))
                .Column(x => x.Text, cm => cm.WithName("text"))
                .Column(x => x.CreatedAt, cm => cm.WithName("created_at"))
        );
        MappingConfiguration.Global.Define(
            new Map<UserDbo>()
                .TableName("users")
                .PartitionKey(x => x.Id)
                .Column(x => x.Id, cm => cm.WithName("id"))
                .Column(x => x.ProviderId, cm => cm.WithName("provider_id"))
                .Column(x => x.Email, cm => cm.WithName("email"))
                .Column(x => x.DisplayName, cm => cm.WithName("display_name"))
                .Column(x => x.AvatarUrl, cm => cm.WithName("avatar_url"))
                .Column(x => x.CreatedAt, cm => cm.WithName("created_at"))
        );
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var hosts = configuration.GetSection("Db:Hosts").GetChildren().Select(c => c.Value);
        var cluster = Cluster.Builder().AddContactPoints(hosts).WithDefaultKeyspace("chat").Build();

        var session = cluster.Connect();
        session.ChangeKeyspace("chat");

        services.AddSingleton(session);
        services.AddSingleton<IMessagesRepository, MessageRepository>();
        services.AddSingleton<IUsersRepository, UsersRepository>();
        services.AddSingleton<IChannelsRepository, ChannelsRepository>();
        services.AddSingleton<INotificationService, NotificationService>();
        ConfigureKafkaProducer(services, configuration);
        ConfigureKafkaConsumer(services, configuration);

        return services;
    }

    private static void ConfigureKafkaProducer(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var producerConfig = configuration.GetSection("Kafka:Producer").Get<ProducerConfig>();

        var producer = new ProducerBuilder<string, NewMessageNotification>(producerConfig)
            .SetValueSerializer(new NewMessageNotificationSerializer())
            .Build();

        services.AddSingleton(producer);
    }

    private static void ConfigureKafkaConsumer(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var consumerConfig = configuration.GetSection("Kafka:Consumer").Get<ConsumerConfig>();

        var consumer = new ConsumerBuilder<string, NewMessageNotification>(consumerConfig)
            .SetValueDeserializer(new NewMessageNotificationSerializer())
            .Build();

        services.AddSingleton(consumer);
    }
}
