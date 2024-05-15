using HouseRenting.Common.Dto.Services;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;

namespace HouseRenting.AdminApp.Pages;

public class StatisticComponent : ComponentBase
{
    [Inject]
    private IServicesClient ServiceClient { get; set; }

    public List<StatisticDto> Statistics = new();

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }

    protected async Task Load()
    {
        var data = await ServiceClient.GetStatistic();
        Statistics = data.Statistics;
    }
}
