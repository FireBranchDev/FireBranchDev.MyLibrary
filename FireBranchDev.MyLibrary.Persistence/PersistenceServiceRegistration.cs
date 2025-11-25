using FireBranchDev.MyLibrary.Application.Contracts.Persistence;
using FireBranchDev.MyLibrary.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FireBranchDev.MyLibrary.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyLibraryDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("FireBranchDevMyLibrary")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
