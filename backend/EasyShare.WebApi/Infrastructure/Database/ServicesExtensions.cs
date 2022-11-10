using Microsoft.EntityFrameworkCore;

namespace EasyShare.WebApi.Infrastructure.Database;

public static class ServicesExtensions
{
    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStrings = new ConnectionStrings();
        configuration.GetSection(ConnectionStrings.SectionName).Bind(connectionStrings);

        services.AddDbContext<DatabaseContext>(ConfigureOptions(connectionStrings));
        services.AddDbContextFactory<DatabaseContext>(ConfigureOptions(connectionStrings), ServiceLifetime.Transient);

        return services;
    }

    private static Action<DbContextOptionsBuilder> ConfigureOptions(ConnectionStrings connectionStrings)
    {
        return optionsAction =>
        {
            optionsAction
                .EnableDetailedErrors()
                .UseSqlServer(connectionStrings.DbConnectionString);
        };
    }
}