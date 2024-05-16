using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;

namespace HouseRenting.ClientApp.Pages;

public class EditRequestComponent : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

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

    [Inject]
    protected IRequestsClient RequestClient { get; set; }

    [Parameter]
    public dynamic Id { get; set; }

    [Parameter]
    public bool CanEdit { get; set; }

    public UpdateRequestRequestDto _request;

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }
    protected async Task Load()
    {
        var result = await RequestClient.GetRequest(Guid.Parse($"{Id}"));
        _request = new();
        _request.Id = result.Id;
        _request.AreaLess = result.AreaLess;
        _request.AreaGreater = result.AreaGreater;
        _request.RoomsCountLess = result.RoomsCountLess;
        _request.RoomsCountGreater = result.RoomsCountGreater;
        _request.PriceLess = result.PriceLess;
        _request.PriceGreater = result.PriceGreater;
        _request.Comment = result.Comment;
        _request.MonthCount = result.MonthCount;
    }

    protected async Task Form0Submit()
    {
        try
        {
            await RequestClient.UpdateRequest(_request);
            DialogService.Close(_request);
        }
        catch (Exception ex)
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"{ex}" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }
}
