using MakerAPI.Services;

namespace MakerAPI;

public static class MakerApiExtensions
{
    public static IApplicationBuilder UseMaker(this IApplicationBuilder app)
    {
        // Initialize Market Connection
        var marketService = app.ApplicationServices.GetService<IExchangeService>();
        var logger = app.ApplicationServices.GetService<ILogger<object>>();
        logger.LogInformation("Maker enabled");
        var makerStarted = marketService.Run();
        if (makerStarted)
            logger.LogInformation("Maker initialized");
        else 
            logger.LogCritical("Maker initialization failed. THERE IS NO CONNECTION TO MARKET. STRATEGIES CANNOT BE STARTED");
        return app;
    }
}