using Application.Contracts;
using Application.Users.UseCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Extension
{
    public static class ServicesLifeTime
    {
        public static IServiceCollection AddServicesLifeTime(
            this IServiceCollection Services)
        {
            Services.AddScoped<LoginUser>();
            Services.AddScoped<RegisterUser>();
            return Services;

        }
    }
}
