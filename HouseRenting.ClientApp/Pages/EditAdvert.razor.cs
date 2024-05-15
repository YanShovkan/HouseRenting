using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Refit;
using System.Text.RegularExpressions;

namespace HouseRenting.ClientApp.Pages;

public class EditAdvertComponent : ComponentBase
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
    protected IAdvertsClient AdvertClient { get; set; }

    [Parameter]
    public dynamic Id { get; set; }

    [Parameter]
    public bool CanEdit { get; set; }

    UpdateAdvertRequestDto _advert;
    protected UpdateAdvertRequestDto advert
    {
        get
        {
            return _advert;
        }
        set
        {
            if (!Equals(_advert, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "advert", NewValue = value, OldValue = _advert };
                _advert = value;
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
        var postGetAdvertByIdResult = await AdvertClient.GetAdvert(Guid.Parse($"{Id}"));
        _advert = new UpdateAdvertRequestDto();
        _advert.Id = postGetAdvertByIdResult.Id;
        _advert.Floor = postGetAdvertByIdResult.Floor;
        _advert.RoomsCount = postGetAdvertByIdResult.RoomsCount;
        _advert.Price = postGetAdvertByIdResult.Price;
        _advert.Area = postGetAdvertByIdResult.Area;
        _advert.CreatedOn = postGetAdvertByIdResult.CreatedOn;
        _advert.Address = postGetAdvertByIdResult.Address;
        _advert.ClientFIO = postGetAdvertByIdResult.ClientFIO;
        _advert.ClientEmail = postGetAdvertByIdResult.ClientEmail;
        _advert.UserId = postGetAdvertByIdResult.UserId;
        _advert.IsAdminReviewed = false;
    }

    protected async Task Form0Submit()
    {
        try
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(_advert.ClientEmail).Success)
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Проверьте корректность введенного email!" });
                return;
            }

            await AdvertClient.UpdateAdvert(_advert);
            DialogService.Close(_advert);
        }
        catch(Exception ex)
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"{ex}" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }
}
