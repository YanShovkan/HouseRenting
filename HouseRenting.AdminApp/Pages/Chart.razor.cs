using HouseRenting.Common.Dto.Order;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace HouseRenting.AdminApp.Pages;

public class ChartComponent : ComponentBase
{
    public record DataItem(string Month, decimal Sum);

    [Inject]
    protected IServicesClient ServiceClient { get; set; } = null!;

    public List<DataItem> MonthOrders = new();
    
    public List<DataItem> Predict = new();

    public List<SeriesColorRange> FillRange = new();

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }

    protected async Task Load()
    {
        var data = await ServiceClient.GetPredict(Online.Admin.Id);

        foreach(var item in data.MonthOrders)
        {
            MonthOrders.Add(new(item.Key, item.Value));
            Predict.Add(new(item.Key, data.Predict));
        }
        
        FillRange.Add(new() { Min = 0, Max = (double)data.Predict * 8 / 10, Color = "red" });
        FillRange.Add(new() { Min = (double)data.Predict * 8 / 10, Max = double.MaxValue, Color = "green" });
    }

}
