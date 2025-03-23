using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();

    //app.ApplyMigrations();

    // REMARK: Uncomment if you want to seed initial data.
    // app.SeedData();
}

app.UseHttpsRedirection();

//app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();

