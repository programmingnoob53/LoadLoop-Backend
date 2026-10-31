using LoadLoop.api.Extensions;
using LoadLoop.api.Models;
using LoadLoop.api.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//add validation
builder.Services.AddValidation();
//add the database
builder.Services.AddDbContext<LoadLoopDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//add the services

builder.Services.AddScoped<UserService>();

var app = builder.Build();
app.MapUsersEndpoint();
app.MapTruckEndpoints();

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
