using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Refit;

namespace HouseRenting.ClientApp.Pages;

public class AddRequestComponent : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

    public void OnPropertyChanged(PropertyChangedEventArgs args)
    {
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

    CreateRequestRequestDto _request;
    protected CreateRequestRequestDto request
    {
        get
        {
            return _request;
        }
        set
        {
            if (!object.Equals(_request, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "request", NewValue = value, OldValue = _request };
                _request = value;
                OnPropertyChanged(args);
                Reload();
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }

    protected async Task Load()
    {
        request = new CreateRequestRequestDto() { UserId = Online.Client.Id };
    }

    protected async Task Button1Click(MouseEventArgs args)
    {
        try
        {
            await RequestClient.CreateRequest(_request);
            DialogService.Close(_request);
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Невозможно создать новую заявку!" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }
}
