using HouseRenting.Common.Dto.Services;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;


namespace HouseRenting.AdminApp.Pages;

public class ReportComponent : ComponentBase
{
    public GenerateReportDto ReportDto { get; set; } = new GenerateReportDto();

    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }
    
    [Inject]
    protected IServicesClient ServicesClient { get; set; }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    public async Task DownloadFileFromStream()
    {
        var file = await ServicesClient.GetReport(ReportDto);
        var fileName = "report.xlsx";
        Stream stream = new MemoryStream(file);
        using var streamRef = new DotNetStreamReference(stream: stream);

        await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }

    public void ExitFromPage()
    {
        UriHelper.NavigateTo("./");
    }

}
