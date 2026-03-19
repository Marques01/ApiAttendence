using Application.Services;
using Application.Services.Interfaces;
using Domain.Repository.Interfaces;
using Infraestructure.Repository;

namespace API.Configurations
{
    public class DependencyInjection
    {
        public static void ApplyConfigurations(IServiceCollection services)
        {
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
