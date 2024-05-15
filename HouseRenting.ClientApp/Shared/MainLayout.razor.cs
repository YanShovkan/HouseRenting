using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HouseRenting.ClientApp.Shared;

public class MainLayoutComponent : LayoutComponentBase
{
    protected RadzenBody body0 = null!;
    protected RadzenSidebar sidebar0 = null!;

    protected async Task SidebarToggle0Click(dynamic args)
    {
        await InvokeAsync(() => { sidebar0.Toggle(); });

        await InvokeAsync(() => { body0.Toggle(); });
    }
}
