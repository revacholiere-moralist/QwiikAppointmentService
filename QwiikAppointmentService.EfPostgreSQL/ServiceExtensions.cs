using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
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
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPublicHolidayRepository, PublicHolidayRepository>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<QwiikAppointmentServiceDataContext>()
                .AddDefaultTokenProviders();
        }
    }
}
