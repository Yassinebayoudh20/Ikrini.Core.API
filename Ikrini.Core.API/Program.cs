// ---------------------------------------------------------------
//   Copyright � Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Ikrini.Core.API.Brokers.Datetimes;
using Ikrini.Core.API.Brokers.Loggings;
using Ikrini.Core.API.Brokers.Storages;
using Ikrini.Core.API.Services.Foundations.Cars;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddControllers();
webApplicationBuilder.Services.AddLogging();
webApplicationBuilder.Services.AddDbContext<StorageBroker>();

webApplicationBuilder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
webApplicationBuilder.Services.AddTransient<IStorageBroker, StorageBroker>();
webApplicationBuilder.Services.AddTransient<IDatetimeBroker, DatetimeBroker>();
webApplicationBuilder.Services.AddTransient<ICarService, CarService>();


WebApplication webApplication = webApplicationBuilder.Build();

webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();
webApplication.Run();