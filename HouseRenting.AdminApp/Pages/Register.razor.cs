using HouseRenting.Common.Dto.Admin;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System.Text.RegularExpressions;

namespace HouseRenting.AdminApp.Pages;

public class RegisterComponent : ComponentBase
{

    [Inject]
    protected NotificationService NotificationService { get; set; } = null!;

    [Inject]
    private IAdminsClient _adminClient { get; set; } = null!;

    [Inject]
    private DialogService DialogService { get; set; } = null!;

    public CreateAdminRequestDto model { get; set; } = new();

    public async Task RegisterAsync(MouseEventArgs args)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!regex.Match(model.Email).Success)
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Проверьте корректность данных!" });
            return;
        }

        try
        {
            await _adminClient.CreateAdmin(model);
            DialogService.Close(model);
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка", Detail = $"Проверьте корректность данных!" });
        }
    }
}
