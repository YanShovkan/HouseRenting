using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Radzen;
using HouseRenting.Common.Dto.Request;
using HouseRenting.Common.Enums;
using HouseRenting.Common.Dto.Advert;
using System.Reflection;

namespace HouseRenting.AdminApp.Pages;

public class RequestsComponent : ComponentBase
{
    private bool _isSorted;
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; } = null!;

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

    [Inject]
    private IRequestsClient RequestsClient { get; set; } = null!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    private NavigationManager UriHelper { get; set; } = null!;

    [Inject]
    private DialogService DialogService { get; set; } = null!;

    [Inject]
    private TooltipService TooltipService { get; set; } = null!;

    [Inject]
    private ContextMenuService ContextMenuService { get; set; } = null!;

    [Inject]
    private NotificationService NotificationService { get; set; } = null!;

    protected RadzenDataGrid<GetRequestResponseDto> grid0 = null!;

    protected IEnumerable<GetRequestResponseDto> _getRequestsResult = null!;
    protected IEnumerable<GetRequestResponseDto> getRequestsResult
    {
        get
        {
            return _getRequestsResult;
        }
        set
        {
            if (!object.Equals(_getRequestsResult, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getOrdersResult", NewValue = value, OldValue = _getRequestsResult };
                _getRequestsResult = value;
                Reload();
            }
        }
    }

    int _getRequestsCount;
    protected int getRequestsCount
    {
        get
        {
            return _getRequestsCount;
        }
        set
        {
            if (!object.Equals(_getRequestsCount, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getOrdersCount", NewValue = value, OldValue = _getRequestsCount };
                _getRequestsCount = value;
                Reload();
            }
        }
    }

    protected async Task Grid0LoadData(LoadDataArgs args)
    {
        try
        {
            if (!_isSorted)
            {
                var postGetAdvertsResult = await RequestsClient.GetRequests();
                getRequestsResult = postGetAdvertsResult.Requests.Where(_ => _.Status != RequestStatus.Denied).AsODataEnumerable();
                getRequestsCount = postGetAdvertsResult.Requests.Count;
            }
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно загрузить данные о заявках!" });
        }
    }

    protected async Task SubmitRequest(MouseEventArgs args, dynamic data)
    {
        try
        {
            if (Enum.Parse<RequestStatus>($"{data.Status}") == RequestStatus.NotReviewed)
            {
                if (await DialogService.Confirm("Вы хотите взять заявку в работу?", "Подтверждение") == true)
                {
                    await RequestsClient.UpdateStatusRequest(Guid.Parse($"{data.Id}"), RequestStatus.InWork);
                    await grid0.Reload();
                }
            }
            else if (Enum.Parse<RequestStatus>($"{data.Status}") == RequestStatus.InWork)
            {
                if (await DialogService.Confirm("Вы хотите пометить заявку рассмотренной?", "Подтверждение") == true)
                {
                    await RequestsClient.UpdateStatusRequest(Guid.Parse($"{data.Id}"), RequestStatus.Confirmed);
                    await grid0.Reload();
                }
            }


        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно пометить заявку рассмотренной!" });
        }
    }

    protected async Task Denie(MouseEventArgs args, dynamic data)
    {
        try
        {
            if (await DialogService.Confirm("Вы хотите отклонить заявку?", "Подтверждение") == true)
            {
                await RequestsClient.UpdateStatusRequest(Guid.Parse($"{data.Id}"), RequestStatus.Denied);
                await grid0.Reload();
            }
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно пометить заявку рассмотренной!" });
        }
    }

    public void OnSort(DataGridColumnSortEventArgs<GetRequestResponseDto> args)
    {
        var sortOrder = args.SortOrder;
        var sortProperty = args.Column.GetSortProperty();
        PropertyInfo prop = typeof(GetRequestResponseDto).GetProperty(sortProperty)!;

        if (sortOrder == SortOrder.Descending)
        {
            _getRequestsResult = _getRequestsResult.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }
        if (sortOrder == SortOrder.Ascending)
        {
            _getRequestsResult = _getRequestsResult.OrderBy(x => prop.GetValue(x, null)).ToList();
        }
        _isSorted = true;
    }
}
