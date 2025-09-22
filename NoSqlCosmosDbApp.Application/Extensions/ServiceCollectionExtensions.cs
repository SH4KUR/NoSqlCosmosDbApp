using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NoSqlCosmosDbApp.Application.Services;
using NoSqlCosmosDbApp.Domain.Interfaces;
using NoSqlCosmosDbApp.Infrastructure.Extensions;

namespace NoSqlCosmosDbApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IUserService, UserService>();

        services.AddInfrastructure(configuration);

        return services;
    }
}