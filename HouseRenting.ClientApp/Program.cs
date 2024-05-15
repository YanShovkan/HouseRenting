using HouseRenting.ClientApp;
using HouseRenting.Common.RefitContracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services
    .AddRefitClient<IAdvertsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5555"));
builder.Services
    .AddRefitClient<IServicesClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5555"));
builder.Services
    .AddRefitClient<IOrderClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5555"));
builder.Services
    .AddRefitClient<IUsersClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5555"));
builder.Services
    .AddRefitClient<IRequestsClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5555"));

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();