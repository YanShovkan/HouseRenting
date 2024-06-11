using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen.Blazor;

namespace HouseRenting.AdminApp.Shared;

public class MainLayoutComponent : LayoutComponentBase
{
    protected RadzenBody body0 = null!;
    protected RadzenSidebar sidebar0 = null!;

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected IServicesClient ServicesClient { get; set; }

    protected async Task SidebarToggle0Click(dynamic args)
    {
        await InvokeAsync(() => { sidebar0.Toggle(); });

        await InvokeAsync(() => { body0.Toggle(); });
    }

    public async Task DownloadFileFromStream()
    {
        var file = await ServicesClient.GetReport();
        var fileName = "avitofile.xlsx";
        Stream stream = new MemoryStream(file);
        using var streamRef = new DotNetStreamReference(stream: stream);

        await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }
}

