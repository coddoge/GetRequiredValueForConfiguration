using GetRequiredValueForConfiguration.Test.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton(hostContext.HostingEnvironment.GetAppConfiguration());
    }).Build();

var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

await host.StartAsync();
var configuration = host.Services.GetRequiredService<IConfiguration>();
{
    logger.LogInformation("Enter normal test");
    var normal = configuration.GetRequiredNoEmptyValue("AAA:BBB");
    logger.LogInformation($" ===> {normal} , Expected CCC");
    logger.LogInformation("Leave normal test");

    logger.LogInformation("Enter empty test");
    try
    {
        var empty = configuration.GetRequiredNoEmptyValue("AAA:EMPTY");
        logger.LogError($" ===> {empty} , Expected Exception");
    }
    catch (Exception e)
    {
        logger.LogInformation($"Expected Exception =====> {e.Message}");
    }
    logger.LogInformation("Leave empty test");

    logger.LogInformation("Enter null test");
    try
    {
        var nul = configuration.GetRequiredNoEmptyValue("AAA:NULL");
        logger.LogError($" ===> {nul} , Expected Exception");
    }
    catch (Exception e)
    {
        logger.LogInformation($"Expected Exception =====> {e.Message}");
    }
    logger.LogInformation("Leave null test");


}
logger.LogInformation("Press ENTER to stop...");
Console.ReadLine();

lifetime.StopApplication();
await host.WaitForShutdownAsync();
