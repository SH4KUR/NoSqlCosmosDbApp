using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NoSqlCosmosDbApp.Domain.Interfaces;
using NoSqlCosmosDbApp.Infrastructure.Repositories;

namespace NoSqlCosmosDbApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddScoped<IUserRepository, UserRepository>();

        var databaseName = configuration["CosmosDb:Database"] ?? throw new NullReferenceException("CosmosDb Database config is null");
        var endpoint = configuration["CosmosDb:Endpoint"] ?? throw new NullReferenceException("CosmosDb Endpoint config is null");
        var primaryKey = configuration["CosmosDb:PrimaryKey"] ?? throw new NullReferenceException("CosmosDb PrimaryKey config is null");

        services.AddDbContextFactory<IdentityDbContext>(optionsBuilder =>
            optionsBuilder
                .UseCosmos(
                    accountEndpoint: endpoint,
                    accountKey: primaryKey,
                    databaseName: databaseName,
                    cosmosOptionsAction: options =>
                    {
                        options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
                        options.MaxRequestsPerTcpConnection(16);
                        options.MaxTcpConnectionsPerEndpoint(32);
                    }));

        return services;
    }
}