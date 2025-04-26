using QwiikAppointmentService.Application;
using QwiikAppointmentService.EfPostgreSQL;
using QwiikAppointmentService.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure inner layers
builder.Services.ConfigureDataContext(builder.Configuration.GetConnectionString("QwiikAppointmentServiceDataContext"));
builder.Services.ConfigureRepositories();
builder.Services.ConfigureApplication();

// Custom configs
builder.Services.ConfigureCorsPolicy();
builder.Services.ConfigureApiVersion();
builder.Services.ConfigureSwagger();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseVersionedSwagger();
app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
