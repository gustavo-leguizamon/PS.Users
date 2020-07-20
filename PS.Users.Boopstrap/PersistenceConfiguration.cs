using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PS.Users.Repositories.Contexts;

namespace PS.Users.Boopstrap
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection PersistenciaConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IApplicationBuilder PersistenciaConfigure(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<UsersContext>())
                {
                    context.Database.Migrate();
                }
            }
            return app;
        }
    }
}
