using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Refit;

namespace HouseRenting.AdminApp.Pages;

public class AddOrderComponent : ComponentBase
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
    protected IAdvertsClient AdvertClient { get; set; }

    [Inject]
    protected IOrderClient OrderClient { get; set; }

    IEnumerable<GetAdvertResponseDto> _getAdvertsResult;
    protected IEnumerable<GetAdvertResponseDto> getAdvertsResult
    {
        get
        {
            return _getAdvertsResult;
        }
        set
        {
            if (!object.Equals(_getAdvertsResult, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getAdvertsResult", NewValue = value, OldValue = _getAdvertsResult };
                _getAdvertsResult = value;
                Reload();
            }
        }
    }

    CreateOrderRequestDto _order;
    protected CreateOrderRequestDto order
    {
        get
        {
            return _order;
        }
        set
        {
            if (!object.Equals(_order, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "order", NewValue = value, OldValue = _order };
                _order = value;
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
        var postGetAdvertsResult = await AdvertClient.GetAdverts(new());
        getAdvertsResult = postGetAdvertsResult.Adverts.Where(_ => _.IsActual && _.ReviewStatus == Common.Enums.ReviewStatus.Reviewed).AsODataEnumerable();

        order = new CreateOrderRequestDto() { AdminId = Online.Admin.Id };
    }

    protected async Task Button1Click(MouseEventArgs args)
    {
        try
        {
            order.MounthPrice = getAdvertsResult.First(_ => _.Id == order.AdvertId).Price;
            await OrderClient.CreateOrder(order);
            DialogService.Close(order);
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to create new Order!" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }
}
