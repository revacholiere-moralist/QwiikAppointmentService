using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace QwiikAppointmentService.WebAPI.Configurations
{
    public static class SwaggerConfiguration
    {
        private const string Title = "Qwiik Appointment Service API";
        private static readonly string[] Versions = { "v1" };

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());

                foreach (var version in Versions)
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = Title,
                        Version = version
                    });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put JWT Token only.",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>()}
            });
            });
        }

        public static void UseVersionedSwagger(this WebApplication app)
        {
            app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = Title;
                foreach (var version in Versions) options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            });
        }
    }
}
