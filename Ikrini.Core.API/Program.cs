// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddControllers();

WebApplication webApplication = webApplicationBuilder.Build();

webApplication.UseHttpsRedirection();
webApplication.UseAuthorization();
webApplication.MapControllers();
webApplication.Run();