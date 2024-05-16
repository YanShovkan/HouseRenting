using HouseRenting.ClientApp;
using HouseRenting.Common.Dto.Advert;
using HouseRenting.Common.Dto.Services;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HouseRenting.ClientApp.Pages;

public class AddAdvertComponent : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; } = null!;

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    protected NavigationManager UriHelper { get; set; } = null!;

    [Inject]
    protected DialogService DialogService { get; set; } = null!;

    [Inject]
    protected TooltipService TooltipService { get; set; } = null!;

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; } = null!;

    [Inject]
    protected NotificationService NotificationService { get; set; } = null!;

    [Inject]
    protected IAdvertsClient _advertClient { get; set; } = null!;

    public CreateAdvertRequestDto _advert = new() { IsAdmin = false, UserId = Online.Client.Id, ClientFIO = Online.Client.Name, ClientEmail = Online.Client.Email };

    private Dictionary<string, string> Files = new();
    protected async Task Button1Click(MouseEventArgs args)
    {
        try
        {
            _advert.MediaFiles.AddRange(Files.Values);
            await _advertClient.CreateAdvert(_advert);
            DialogService.Close(_advert);
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to create new Advert!" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        DialogService.Close(null);
    }

    protected async Task OnClientChange(UploadChangeEventArgs args)
    {
        var newFiles = new List<string>();

        foreach (var file in args.Files)
        {
            try
            {
                Stream fileStream;
                try
                {
                    fileStream = file.OpenReadStream();
                }
                catch
                {
                    newFiles.Add(file.Name);
                    continue;
                }

                var fileString = await GetBase64ForFile(fileStream);
                newFiles.Add(file.Name);
                Files.TryAdd(file.Name, fileString);
            }
            catch
            {
                NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Ошибка!", Detail = $"Не удалось загрузить фото!" });
            }
        }

        foreach(var key in Files.Keys)
        {
            if (!newFiles.Contains(key))
            {
                Files.Remove(key);
            }
        }

    }

    private async Task<string> GetBase64ForFile(Stream file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var bytes = stream.ToArray();
        var response = Convert.ToBase64String(bytes);
        file.Close();
        return response;
    }
}
