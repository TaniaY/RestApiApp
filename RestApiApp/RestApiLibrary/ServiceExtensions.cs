using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RestApiLibrary
{
    public static class ServiceConfiguration
    {
        public static void AddCustomServices<TContext>(this IServiceCollection services, IConfiguration configuration)
             where TContext : DbContext
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }

        public static void UseCustomMiddleware(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
