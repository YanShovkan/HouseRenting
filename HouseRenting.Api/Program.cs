using Autofac.Extensions.DependencyInjection;
using HouseRenting.Api;
using HouseRenting.Dal;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateHostBuilder(args).Build();

        using var scope = builder.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();

        builder.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseServiceProviderFactory(new AutofacServiceProviderFactory());
}

