using API.Data;
using API.Interface;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    /*when we make a class static, that means we can use 
    the methods inside it without instantiating*/
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
          //initialising dbcontext server
          services.AddDbContext<DataContext>(opt => 
            {
                //connection string
                opt.UseSqlite(config.GetConnectionString("defaultConnections"));
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }   
    }
}