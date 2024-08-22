using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.App.Components;

public sealed class InitializeTimeZone : ComponentBase
{
    [Inject]
    public BrowserTimeProvider TimeProvider { get; set; } = default!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && TimeProvider is { IsLocalTimeZoneSet: false } browserTimeProvider)
        {
            try
            {
                await using var module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./timezone.js"
                );
                var timeZone = await module.InvokeAsync<string>("getBrowserTimeZone");
                browserTimeProvider.SetBrowserTimeZone(timeZone);
            }
            catch (JSDisconnectedException) { }
        }
    }
}
