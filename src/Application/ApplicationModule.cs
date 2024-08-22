using Application.Channels;
using Application.Messages;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMessagesService, MessagesService>();
        services.AddSingleton<IUsersService, UsersService>();
        services.AddSingleton<IChannelsService, ChannelsService>();

        return services;
    }
}
