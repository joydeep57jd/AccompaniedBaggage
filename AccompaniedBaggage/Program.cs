using Microsoft.EntityFrameworkCore;
using SezApi.Data;
using SezApi.Services;
using Serilog;
using System;

var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"log-{DateTime.Now:yyyyMMdd_HHmmss}.txt");
Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Error()
    .WriteTo.File(
        path: logPath,
        rollingInterval: RollingInterval.Day,          
        retainedFileCountLimit: 7,                    
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);

    //For seriLog
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddDbContext<AccompaniedBaggageDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IServices, Services>();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();


    if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllers();

   // Log.Information("Application starting up");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}