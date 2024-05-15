using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.Order;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Refit;

namespace HouseRenting.AdminApp.Pages;

public class EditOrderComponent : ComponentBase
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
    protected IOrderClient OrderClient { get; set; }

    [Inject]
    protected IAdvertsClient AdvertClient { get; set; }

    [Parameter]
    public dynamic Id { get; set; }

    [Parameter]
    public bool CanEdit { get; set; }

    UpdateOrderRequestDto _order;
    protected UpdateOrderRequestDto order
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
                OnPropertyChanged(args);
                Reload();
            }
        }
    }

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
        var orderDto = await OrderClient.GetOrder(Guid.Parse($"{Id}"));
        _order = new UpdateOrderRequestDto();
        _order.Id = orderDto.Id;
        _order.MeetTime = orderDto.MeetTime;
        _order.StartDate = orderDto.StartDate;
        _order.AdvertId = orderDto.Advert.Id;
        _order.AgencyPercent = orderDto.AgencyPercent;
        _order.MounthCount = orderDto.MounthCount;
        _order.MounthPrice = orderDto.MounthPrice;
        _order.OldAdvertId = orderDto.Advert.Id;
        _order.AdminId = orderDto.Admin.Id;

        var postGetAdvertsResult = await AdvertClient.GetAdverts(new());
        getAdvertsResult = postGetAdvertsResult.Adverts.Where(_ => (_.IsActual && _.ReviewStatus == Common.Enums.ReviewStatus.Reviewed) || _.Id == _order.AdvertId).AsODataEnumerable();
    }

    protected async Task Button1Click(MouseEventArgs args)
    {
        try
        {
            await OrderClient.UpdateOrder(order);
            DialogService.Close(order);
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to update Order" });
        }
    }
    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }
}
