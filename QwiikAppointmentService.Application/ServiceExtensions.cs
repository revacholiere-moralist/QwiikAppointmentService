﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QwiikAppointmentService.Application.Behaviours;
using System.Reflection;

namespace QwiikAppointmentService.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
        }
    }
}
