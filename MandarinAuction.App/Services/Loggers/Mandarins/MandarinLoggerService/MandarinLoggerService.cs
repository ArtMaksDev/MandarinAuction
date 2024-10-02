#region usings

using Microsoft.Extensions.Logging;

#endregion

namespace MandarinAuction.App.Services.Loggers.Mandarins.MandarinLoggerService;

public class MandarinLoggerService: IMandarinLoggerService
{
    private readonly ILogger<MandarinLoggerService> _logger;

    public MandarinLoggerService(ILogger<MandarinLoggerService> logger)
    {
        _logger = logger;
    }

    public void LogMandarinCreated(Guid mandarinId)
    {
        _logger.LogInformation("Мандарин с ID {MandarinId} был создан успешно.", mandarinId);
    }
}