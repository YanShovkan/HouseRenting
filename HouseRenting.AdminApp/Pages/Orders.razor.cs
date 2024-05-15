using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Radzen;
using HouseRenting.Common.Dto.Order;
using System.Reflection;

namespace HouseRenting.AdminApp.Pages;

public class OrdersComponent : ComponentBase
{
    private bool _isSorted = false;
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; } = null!;

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

    [Inject]
    private IOrderClient _orderClient { get; set; } = null!;

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

    protected RadzenDataGrid<GetOrderResponseDto> grid0 = null!;

    protected IEnumerable<GetOrderResponseDto> _getOrdersResult = null!;
    protected IEnumerable<GetOrderResponseDto> getOrdersResult
    {
        get
        {
            return _getOrdersResult;
        }
        set
        {
            if (!object.Equals(_getOrdersResult, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getOrdersResult", NewValue = value, OldValue = _getOrdersResult };
                _getOrdersResult = value;
                Reload();
            }
        }
    }

    int _getOrdersCount;
    protected int getOrdersCount
    {
        get
        {
            return _getOrdersCount;
        }
        set
        {
            if (!object.Equals(_getOrdersCount, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getOrdersCount", NewValue = value, OldValue = _getOrdersCount };
                _getOrdersCount = value;
                Reload();
            }
        }
    }

    protected async Task Button0Click(MouseEventArgs args)
    {
        var dialogResult = await DialogService.OpenAsync<AddOrder>("Добавить сделку", null);
        await grid0.Reload();

        await InvokeAsync(() => { StateHasChanged(); });
    }

    protected async Task Grid0LoadData(LoadDataArgs args)
    {
        try
        {
            if (!_isSorted)
            {
                var postGetAdvertsResult = await _orderClient.GetOrders(Online.Admin.Id);
                getOrdersResult = postGetAdvertsResult.Orders.AsODataEnumerable();
                getOrdersCount = postGetAdvertsResult.Orders.Count;
            }
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно загрузить данные о сделках!" });
        }
    }

    protected async Task Grid0RowSelect(GetOrderResponseDto args)
    {
        var dialogResult = await DialogService.OpenAsync<EditOrder>("Изменить сделку", new Dictionary<string, object>() { { "Id", args.Id }, { "CanEdit", args.IsCommited } });
        await grid0.Reload();

        await InvokeAsync(() => { StateHasChanged(); });
    }

    protected async Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
    {
        try
        {
            if (await DialogService.Confirm("Вы хотите удалить сделку?", "Удаление") == true)
            {
                if (data.IsCommited)
                {
                    NotificationService.Notify(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Warning,
                        Summary = $"Проблема",
                        Detail = $"Невозможно удалить закрытую сделку!"
                    });
                    return;
                }
                await _orderClient.DeleteOrder(Guid.Parse($"{data.Id}"));
                await grid0.Reload();
            }
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно удалить сделку!" });
        }
    }

    protected async Task Commit(MouseEventArgs args, dynamic data)
    {
        try
        {
            if (await DialogService.Confirm("Вы хотите закрыть сделку?", "Закрытие") == true)
            {
                if (data.IsCommited)
                {
                    NotificationService.Notify(new NotificationMessage()
                    {
                        Severity = NotificationSeverity.Warning,
                        Summary = $"Проблема",
                        Detail = $"Невозможно закрыть закрытую сделку!"
                    });
                    return;
                }

                await _orderClient.CommitOrder(Guid.Parse($"{data.Id}"));
                await grid0.Reload();
            }
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Невозможно закрыть сделку!" });
        }
    }

    public void OnSort(DataGridColumnSortEventArgs<GetOrderResponseDto> args)
    {
        var sortOrder = args.SortOrder;
        var sortProperty = args.Column.GetSortProperty();
        PropertyInfo prop = typeof(GetOrderResponseDto).GetProperty(sortProperty)!;

        if (sortOrder == SortOrder.Descending)
        {
            _getOrdersResult = _getOrdersResult.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }
        if (sortOrder == SortOrder.Ascending)
        {
            _getOrdersResult = _getOrdersResult.OrderBy(x => prop.GetValue(x, null)).ToList();
        }
        _isSorted = true;
    }
}
