using HouseRenting.Dal;
using HouseRenting.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using HouseRenting.Dal.Repositories.Contracts;
using HouseRenting.Core.Profiles;
using HouseRenting.Dal.Services.Contracts;
using HouseRenting.Dal.Services;
using HouseRenting.Common.CQRS;
using HouseRenting.Core.QueryHandlers.UserHandlers;
using HouseRenting.Core.Services.Contracts;
using HouseRenting.Core.Services;

namespace HouseRenting.Api;

public partial class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        services.AddAutofac();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<DatabaseContext>(c => c.UseNpgsql("Host=localhost;Port=5432;Database=house-renting-db;User Id=postgres;Password=123;"));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                    builder.WithOrigins("http://localhost:5225")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            options.DefaultPolicyName = "AllowAll";
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IPredictService, PredictService>();
        services.AddScoped<IReportGenerator, ReportGenerator>();
        services.AddScoped<IStatisticCollector, StatisticCollector>();
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(GetUserQueryHandler).Assembly)
            .Where(x => x.GetInterfaces().Any(_ => _.IsGenericType && (typeof(ICommandHandler<>) == _.GetGenericTypeDefinition() ||
            typeof(IQueryHandler<,>) == _.GetGenericTypeDefinition())))
            .AsImplementedInterfaces();
    }

}
