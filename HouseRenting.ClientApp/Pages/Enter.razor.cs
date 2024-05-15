using HouseRenting.ClientApp;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace HouseRenting.ClientApp.Pages;

public class EnterComponent : ComponentBase
{
    [Inject]
    private IUsersClient UserClient { get; set; } = null!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private DialogService DialogService { get; set; } = null!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = null!;

    protected string? Login { get; set; }
    protected string? Password { get; set; }

    public async Task EnterInSystem()
    {
        if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Login))
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Надо заполнить пароль и логин! {Login} " });
            return;
        }

        var user = await UserClient.GetUser(login: Login!);

        if (user is null || !user.Password.Equals(Password))
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Неверный логин или пароль!" });
            return;
        }

        Online.Client = user;
        Online.IsOnline = true;
        NavigationManager.NavigateTo("./");
    }

    public async Task Register()
    {
        await DialogService.OpenAsync<Register>("Регистрация", new Dictionary<string, object>() { });
    }
}
