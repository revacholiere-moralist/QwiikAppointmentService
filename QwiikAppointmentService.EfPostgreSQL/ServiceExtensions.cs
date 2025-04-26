using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.EfPostgreSQL.Context;
using QwiikAppointmentService.EfPostgreSQL.Repositories;

namespace QwiikAppointmentService.EfPostgreSQL
{
    public static class ServiceExtensions
    {
        public static void ConfigureDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<QwiikAppointmentServiceDataContext>(opt =>
                opt.UseNpgsql(connectionString));
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
