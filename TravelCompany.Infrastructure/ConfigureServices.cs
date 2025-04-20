using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelCompany.Application.Common.Interfaces;
using TravelCompany.Domain.Settings;
using TravelCompany.Infrastructure.Persistence;

namespace TravelCompany.Infrastructure
{
    static public class ConfigureServices
    {
     
        static public IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

			// services.Configure<DatabaseSettings>(option => option.DefualtString= ConnectionString);

			services.Configure<ConnectionStrings>(option => option.DefaultConnection= configuration!.GetConnectionString("DefaultConnection")!);
			//services.Configure<ConnectionStrings>( configuration.GetSection("ConnectionStrings")) ;
			



            return services;

        }


    }
}
