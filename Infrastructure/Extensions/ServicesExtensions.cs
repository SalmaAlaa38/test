using Application.Contracts;
using Domain.Utilties;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Managers;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.Configure<JWT>(configuration.GetSection("JWT"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                    ?? throw new Exception("Connection string 'DefaultConnection' not found."),
                    sqlServerOptions => sqlServerOptions
                    .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                );
            return services;
        }
    }
}
