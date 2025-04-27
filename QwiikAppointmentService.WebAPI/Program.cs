using Evercare.HealthrecordService.WebApi.Configurations;
using QwiikAppointmentService.Application;
using QwiikAppointmentService.Application.Common.Options;
using QwiikAppointmentService.EfPostgreSQL;
using QwiikAppointmentService.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.ConfigureDataContext(builder.Configuration.GetConnectionString("QwiikAppointmentServiceDataContext"));
builder.Services.ConfigureRepositories();
builder.Services.ConfigureApplication();
builder.Services.ConfigureIdentity();
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
builder.Services.ConfigureJwt(jwtOptions);

// Custom configs
builder.Services.ConfigureCorsPolicy();
builder.Services.ConfigureApiVersion();
builder.Services.ConfigureSwagger();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseVersionedSwagger();
app.UseCors();
app.UseErrorHandler();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
