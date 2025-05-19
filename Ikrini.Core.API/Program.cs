// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Brokers.Datetimes;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);

webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddLogging();
webApplicationBuilder.Services.AddDbContext<StorageBroker>();

// Swagger configuration
webApplicationBuilder.Services.AddEndpointsApiExplorer();
webApplicationBuilder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ikrini Core API",
        Version = "v1",
        Description = "Peer-to-peer car rental platform API",
        Contact = new OpenApiContact
        {
            Name = "Yassine Bayoudh",
            Email = "bayoudh.yassine20@gmail.com",
            Url = new Uri("https://github.com/YassineBayoudh")
        }
    });
});

// Brokers and services
webApplicationBuilder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
webApplicationBuilder.Services.AddTransient<IStorageBroker, StorageBroker>();
webApplicationBuilder.Services.AddTransient<IDatetimeBroker, DatetimeBroker>();
webApplicationBuilder.Services.AddTransient<ICarService, CarService>();

WebApplication webApplication = webApplicationBuilder.Build();

if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseSwagger();
    webApplication.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ikrini Core API V1");
        options.RoutePrefix = string.Empty;
    });
}

webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();
webApplication.Run();
