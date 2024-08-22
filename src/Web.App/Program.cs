using Application;
using Infrastructure;
using Logto.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Web.App;
using Web.App.Components;
using Web.App.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddLogtoAuthentication(options =>
{
    options.Endpoint = builder.Configuration["Logto:Endpoint"]!;
    options.AppId = builder.Configuration["Logto:AppId"]!;
    options.AppSecret = builder.Configuration["Logto:AppSecret"];
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHostedService<MessageNotificationsBackgroundService>();
builder.Services.AddScoped<BrowserTimeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapHub<ChatHub>("/chat");
app.MapGet(
    "/SignIn",
    async context =>
    {
        if (!(context.User?.Identity?.IsAuthenticated ?? false))
        {
            await context.ChallengeAsync(new AuthenticationProperties { RedirectUri = "/" });
        }
        else
        {
            context.Response.Redirect("/");
        }
    }
);

app.MapGet(
    "/SignOut",
    async context =>
    {
        if (context.User?.Identity?.IsAuthenticated ?? false)
        {
            await context.SignOutAsync(new AuthenticationProperties { RedirectUri = "/" });
        }
        else
        {
            context.Response.Redirect("/");
        }
    }
);
app.Run();
