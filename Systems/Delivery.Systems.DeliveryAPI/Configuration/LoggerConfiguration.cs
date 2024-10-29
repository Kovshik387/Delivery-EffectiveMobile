using Delivery.Systems.DeliveryAPI.Settings;
using Serilog.Events;
using Serilog;

namespace Delivery.Systems.DeliveryAPI.Configuration;

public static class LoggerConfiguration
{
    public static void AddAppLogger(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var logSettings = configuration.GetSection("Log").Get<LogSettings>()!;
        
        var loggerConfiguration = new Serilog.LoggerConfiguration();

        loggerConfiguration
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext();

        if (!Enum.TryParse(logSettings.Level, out LogLevels level)) level = LogLevels.Information;

        var serilogLevel = level switch
        {
            LogLevels.Verbose => LogEventLevel.Verbose,
            LogLevels.Debug => LogEventLevel.Debug,
            LogLevels.Information => LogEventLevel.Information,
            LogLevels.Warning => LogEventLevel.Warning,
            LogLevels.Error => LogEventLevel.Error,
            LogLevels.Fatal => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };

        loggerConfiguration
            .MinimumLevel.Is(serilogLevel)
            .MinimumLevel.Override("Microsoft", serilogLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", serilogLevel)
            .MinimumLevel.Override("System", serilogLevel)
            ;

        var logItemTemplate =
            "[{Timestamp:HH:mm:ss:fff} {Level:u3} ({CorrelationId})] {Message:lj}{NewLine}{Exception}";

        if (logSettings.WriteToConsole)
            loggerConfiguration.WriteTo.Console(
                serilogLevel,
                logItemTemplate
            );

        if (logSettings.WriteToFile)
        {
            if (!Enum.TryParse(logSettings.FileRollingInterval, out LogRollingInterval interval))
                interval = LogRollingInterval.Day;

            var serilogInterval = interval switch
            {
                LogRollingInterval.Infinite => RollingInterval.Infinite,
                LogRollingInterval.Year => RollingInterval.Year,
                LogRollingInterval.Month => RollingInterval.Month,
                LogRollingInterval.Day => RollingInterval.Day,
                LogRollingInterval.Hour => RollingInterval.Hour,
                LogRollingInterval.Minute => RollingInterval.Minute,
                _ => RollingInterval.Day
            };


            if (!int.TryParse(logSettings.FileRollingSize, out var size)) size = 5242880;

            var fileName = $"_.log";

            loggerConfiguration.WriteTo.File($"logs/{fileName}",
                serilogLevel,
                logItemTemplate,
                rollingInterval: serilogInterval,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: size
            );
        }

        // Make logger
        var logger = loggerConfiguration.CreateLogger();


        // Apply logger to application
        builder.Host.UseSerilog(logger, true);
    }
}