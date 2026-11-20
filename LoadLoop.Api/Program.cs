using LoadLoop.api.Data;
using LoadLoop.api.Extensions;
using LoadLoop.api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//add validation
builder.Services.AddValidation();
//add the database
builder.Services.AddDbContext<LoadLoopContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//add the services

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TruckService>();
builder.Services.AddScoped<DriverService>();
var app = builder.Build();
app.MapTruckEndPoints();
app.MapUserEndpoints();
app.MapDriverEndpoints();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "LoadLoop API v1"
        );
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
